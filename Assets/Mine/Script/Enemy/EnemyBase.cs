using UnityEngine;

/// <summary>
/// 敵のクラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    // 位置調整
    private protected Vector2 offset = default;
    public Vector2 Offset { get { return offset; } }

    // 分割空間内での位置情報の番号
    private protected int dividedAreaNumber = default;
    public int DividedAreaNumber { get { return dividedAreaNumber; } }

    // HP
    private protected int hp = default;

    // 行動パターン
    private protected int movePattern = default;

    // 移動速度
    private protected float speed = default;

    // 出現からの時間を計測する変数
    private protected float appeardTime = default;

    // 次弾発射の待ち時間
    private protected float bulletShotInterval = default;

    // 射撃パターン
    [SerializeField]
    private protected EnemyShotData shotData;

    // 弾データ
    [SerializeField]
    private protected BulletSpriteData enemyBulletData;

    // 弾速
    private protected float bulletSpeed = default;

    // 弾の色
    private protected int bulletColor = default;

    // 射撃処理が何回行われたか
    private protected int shotUpdateCount = default;

    // アニメーション用の定数・変数群
    #region Animation

    // 画像データ配列を持つ
    private protected Sprite[] idleImagePattern = default;
    private protected Sprite[] rightImagePattern = default;

    // いまどちらの画像データ配列を読んでいるか
    public enum SpriteArray
    {
        idle,
        right,
    }

    private protected SpriteArray loadSpriteArray = default;

    // 画像切り替えの待ち時間
    private const float IMAGE_CHANGE_INTERVAL = 0.4f;

    // 切り替え間隔を計るための変数
    private float imageCountTime = default;

    // 今何番目の画像なのかを格納する変数
    private int imageNumber = default;
    #endregion

    protected EnemyBulletPool _enemyBulletPool = default;

    protected bool _isShot = default;

    private void Awake()
    {
        // 読み込んでいる画像データ配列を初期化
        loadSpriteArray = SpriteArray.idle;

        _enemyBulletPool = GameObject.FindGameObjectWithTag("EnemyBulletPool").GetComponent<EnemyBulletPool>();
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// 敵の射撃
    /// </summary>
    private protected void EnemyShot(EnemyShotData shotData, BulletSpriteData bulletData, int shotCount)
    {
        // ショットパターンを読み込んで生成（パターンごとに弾数が違うのでForループ）
        for (int i = 0; i < shotData.OnceShotBullet; i++)
        {
            Vector2 moveDirection = BulletVectorCalculation.BulletVector(transform.position, shotData, i, shotCount);

            _enemyBulletPool.InstBullet(this.transform.position, bulletData, shotData.BulletSpeed, moveDirection);
        }
    }

    private protected void EnemyMove(int movePattern, float moveSpeed, float appeardTime)
    {
        // 動きデータから動きを読んで反映
        (float addPositionX, float addPositionY) = EnemyMovePattern.MovePatternLoad(transform, appeardTime, movePattern);

        this.transform.position = new Vector3(this.transform.position.x + (addPositionX * moveSpeed),
            this.transform.position.y + (addPositionY * moveSpeed));

        // アニメーションの反映
        // 画像データが無いなら終了
        if (idleImagePattern == null || rightImagePattern == null)
        {
            return;
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}