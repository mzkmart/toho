using System.Collections;
using UnityEngine;

/// <summary>
/// 敵のクラス
/// </summary>
public class BossEnemy : EnemyBase
{
    // ボスのデータ
    BossData bossData;

    // スペルカードを演出するクラス
    //SpellCardEffect spellCardEffect;

    // ボスの戦闘前の定位置
    private const float STANDBY_POSITION_X = 0f;
    private const float STANDBY_POSITION_Y = 4f;

    private const float APPEARD_POSITION_X = 0f;
    private const float APPEARD_POSITION_Y = 8f;

    // ボスの状態の列挙
    enum BossState
    {
        Idle,
        Appear,
        Battle,
        Defeat
    }

    // 発射間隔を計るための変数
    private float shotCountTime = default;

    // 出現時のLerp処理の補完位置（0～1）
    //private float leapNowLocation = default;

    // ウェーブ番号
    private int nowWeveNumber = default;

    // ボスが出現したか
    private bool isBossAppeard = default;

    // 射撃を許可するか
    private bool canBossShot = default;

    // 現在のボスの状態
    [SerializeField] private BossState nowBossState = BossState.Idle;

    Vector2 currentVelocity = default;

    private void FixedUpdate()
    {
        switch (nowBossState)
        {
            case BossState.Idle:

                // 待機時は何もしない
                return;

            case BossState.Appear:

                // 出現時の処理
                AppearUpdate();
                break;

            case BossState.Battle:

                // 戦闘時の処理
                BattleUpdate();
                break;

            case BossState.Defeat:

                // 撃破後の処理
                break;
        }
    }

    /// <summary>
    /// 出現・戦闘待機時の処理
    /// </summary>
    private void AppearUpdate()
    {
        // アニメ適応の為に仮で動かす
        base.EnemyMove((int)EnemyMovePattern.Pattern.Stay, speed, appeardTime);

        // スタンバイ位置へ移動
        if (!isBossAppeard)
        {
            Vector2 currentPos = this.transform.position;


            this.transform.position = currentPos;

            // 移動完了後にフラグを設定
            if (Mathf.Abs(currentVelocity.x) < 0.01f)
            {
                isBossAppeard = true;
            }
        }

        if (isBossAppeard)
        {
            //spellCardEffect = this.GetComponent<SpellCardEffect>();

            StartCoroutine(ChangeWaveEffect());

            // 射撃を許可
            canBossShot = true;

            // 戦闘へ移行
            nowBossState = BossState.Battle;
        }
    }

    /// <summary>
    /// 戦闘中の処理
    /// </summary>
    private void BattleUpdate()
    {
        if (hp <= 0)
        {
            return;
        }

        // 出現からの時間の加算
        appeardTime += Time.deltaTime;

        // 敵の移動
        base.EnemyMove(movePattern, speed, appeardTime);


        // 敵の当たり判定
        int hitCount = 0;

        if (hitCount > 0)
        {
            LifeCalcurate(hitCount);
        }
        // 射撃データがない場合終了
        if (shotData == null)
        {
            return;
        }

        // 敵の射撃
        shotCountTime += Time.deltaTime;

        if (bulletShotInterval < shotCountTime &&
            canBossShot)
        {
            base.EnemyShot(shotData, enemyBulletData, shotUpdateCount);

            shotUpdateCount++;


            shotCountTime = 0;
        }
    }

    /// <summary>
    /// HPの計算とそれに応じた処理をするメソッド
    /// </summary>
    private void LifeCalcurate(int hitCount)
    {
        // 既にHPが0以下の場合は終わる
        if (hp <= 0)
        {
            return;
        }

        // HPを減算
        while (hitCount > 0)
        {
            hp--;
            hitCount--;
        }

        // HPが0以下になった場合に以降の処理を行う
        if (hp > 0)
        {
            return;
        }


        // 次のウェーブへ移行する
        nowWeveNumber++;

        // 次がない場合撃破される
        if (bossData.bossWaveData.Count <= nowWeveNumber)
        {
            // 射撃を中止
            canBossShot = false;

            // 撃破状態へ遷移、演出コルーチンを起動
            nowBossState = BossState.Defeat;
            StartCoroutine(BossDefeatEffect());
            return;
        }

        // 次ウェーブを読み込み・演出
        StartCoroutine(ChangeWaveEffect());
    }

    /// <summary>
    /// マネージャーから有効化される際のデータを適応するメソッド
    /// </summary>
    public void ActiveInitLoad(BossData newBossData)
    {
        // 敵のウェーブ管理データを取得
        bossData = newBossData;

        // ウェーブ番号を初期化
        nowWeveNumber = 0;

        // 現在のアニメーション用に使用する画像データ配列を初期化
        loadSpriteArray = SpriteArray.idle;

        offset = bossData.Offset;

        // 位置を設定
        this.transform.position = new Vector2(STANDBY_POSITION_X, STANDBY_POSITION_Y);

        // アニメーション用の画像
        idleImagePattern = bossData.BossImageIdle;
        rightImagePattern = bossData.BossImageRightMove;

        // 待機状態から移行
        nowBossState = BossState.Appear;
    }

    /// <summary>
    /// ウェーブ開始・変更時のデータを適応するメソッド
    /// </summary>
    /// <param name="bossWaveNumber">ボスデータ内のウェーブ番号</param>
    public void ChangeWaveLoad(int bossWaveNumber)
    {
        // HP・移動パターン
        hp = bossData.bossWaveData[bossWaveNumber].BossHp;
        movePattern = bossData.bossWaveData[bossWaveNumber].BossMovePattern;

        // 射撃用のパラメータ・画像
        shotData = bossData.bossWaveData[bossWaveNumber].BossShotData;

        if (shotData != null)
        {
            bulletSpeed = shotData.BulletSpeed;
            bulletColor = shotData.BulletColorType;
            bulletShotInterval = shotData.ShotIntervalTime;

            // 敵の発射する弾情報を取得
            enemyBulletData = shotData.BulletData;
        }
    }

    /// <summary>
    /// ウェーブが切り替わる際の演出
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeWaveEffect()
    {
        // 射撃を中止
        canBossShot = false;

        // 次のウェーブがスペルカードなら追加演出
        if (bossData.bossWaveData[nowWeveNumber].IsSpellCard)
        {
            yield return new WaitForSeconds(3f);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        // 次のウェーブをロード
        ChangeWaveLoad(nowWeveNumber);

        // 射撃を再開
        canBossShot = true;
    }

    /// <summary>
    /// 被ダメージ時の演出と処理
    /// </summary>
    /// <returns></returns>
    IEnumerator BossDefeatEffect()
    {

        Vector2 currentVelocity = default;

        Vector2 goalPos = new Vector2(this.transform.position.x + 3, this.transform.position.y + 3);

        // 右上に持っていく
        for (Vector2 pos = this.transform.position; pos.y < goalPos.y - 0.01f; pos = Vector2.SmoothDamp(pos, goalPos, ref currentVelocity, 0.05f))
        {
            this.transform.position = pos;

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.1f);


        // 撃破演出
        float scale = this.transform.localScale.x;

        // 徐々に透明かつ大きくする
        for (float alpha = 1; alpha > 0; alpha -= 0.03f)
        {
            scale += 0.1f;
            this.gameObject.transform.localScale = new Vector3(scale, scale, 1);


            yield return new WaitForSeconds(0.01f);
        }

        gameObject.SetActive(false);
    }
}