using UnityEngine;

public class EnemyMove : EnemyBase
{
    // 発射間隔を計るための変数
    private float shotCountTime = default;

    void FixedUpdate()
    {
        shotCountTime += Time.deltaTime;
        // 出現からの時間をカウント
        appeardTime += Time.deltaTime;

        EnemyMove(movePattern, speed, appeardTime);

        if (_isShot)
        {
            if (bulletShotInterval < shotCountTime &&
                appeardTime > shotData.ShotStartTime &&
                appeardTime < shotData.ShotEndTime)
            {
                base.EnemyShot(shotData, enemyBulletData, shotUpdateCount);

                shotUpdateCount++;
                shotCountTime = 0;
                bulletShotInterval = shotData.ShotIntervalTime;
            }
        }
    }

    public void OnEnableInitLoad(EnemyWaveData enemyWaveData, int enemyDataNumber)
    {
        // 現在のアニメーション用に使用する画像データ配列を初期化
        loadSpriteArray = SpriteArray.idle;

        // 時間カウンターを初期化
        appeardTime = 0;
        shotCountTime = 0;

        // 値を持ってくる
        // 基本パラメータ
        hp = enemyWaveData.enemyData[enemyDataNumber].Hp; // 体力
        movePattern = enemyWaveData.enemyData[enemyDataNumber].MovePattern; // 移動パターン
        speed = enemyWaveData.enemyData[enemyDataNumber].Speed; // 速さ

        // 画像と判定のパラメータ
        EnemySpriteData enemySprite = enemyWaveData.enemyData[enemyDataNumber].EnemySpriteData;

        if (enemySprite != null)
        {
            offset = enemySprite.Offset; // 画像と判定をずらす移動量
            idleImagePattern = enemySprite.EnemyImageIdle; // 直進時の画像配列
            rightImagePattern = enemySprite.EnemyImageRightMove; // 左移動時の画像配列
        }
        else
        {
            // データがnullの場合初期化
            offset = default;
            idleImagePattern = default;
            rightImagePattern = default;
        }

        // 射撃用の画像・パラメータ
        shotData = enemyWaveData.enemyData[enemyDataNumber].ShotData;

        if (shotData != null)
        {
            bulletSpeed = shotData.BulletSpeed; // 射撃する弾の速さ
            bulletColor = shotData.BulletColorType; // 射撃する弾の色
            bulletShotInterval = shotData.ShotIntervalTime; // 弾の発射間隔
            enemyBulletData = shotData.BulletData; // 発射する弾の情報
            _isShot = true;
        }
        else
        {
            // データがnullの場合初期化
            bulletSpeed = default;
            bulletColor = default;
            bulletShotInterval = default;
            enemyBulletData = default;
            _isShot = false;
        }

        // 初期位置を反映する際、0がフィールドの中央になるように調整
        this.transform.position = new Vector2(enemyWaveData.enemyData[enemyDataNumber].AppearancePositionX,
            enemyWaveData.enemyData[enemyDataNumber].AppearancePositionY);
    }
}
