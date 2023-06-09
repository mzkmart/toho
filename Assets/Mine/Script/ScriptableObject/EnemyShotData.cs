using UnityEngine;


/// <summary>
/// 敵の弾の射撃情報（ScriptableObject） 
/// </summary>

[CreateAssetMenu(fileName = "EnemyShotDate", menuName = "Data/NewEnemyShotDate")]
public class EnemyShotData : ScriptableObject
{
    // 射撃タイプと弾の軌道
    public BulletVectorCalculation.ShotType shotType;
    public BulletVectorCalculation.AngleType angleType;

    // 弾速
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }

    // 弾の種類
    [SerializeField] private BulletSpriteData bulletData;
    public BulletSpriteData BulletData { get { return bulletData; } set { bulletData = value; } }

    // 弾の色
    [SerializeField] private int bulletColorType;
    public int BulletColorType { get { return bulletColorType; } set { bulletColorType = value; } }

    // 射撃を開始するまでの時間
    [SerializeField] private float shotStartTime;
    public float ShotStartTime { get { return shotStartTime; } set { shotStartTime = value; } }

    // 射撃を終了するまでの時間
    [SerializeField] private float shotEndTime;
    public float ShotEndTime { get { return shotEndTime; } set { shotEndTime = value; } }

    // 発射間隔時間
    [SerializeField] private float shotIntervalTime;
    public float ShotIntervalTime { get { return shotIntervalTime; } set { shotIntervalTime = value; } }

    // N-Way弾の列数
    [SerializeField] private int onceShotBullet;
    public int OnceShotBullet { get { return onceShotBullet; } set { onceShotBullet = value; } }

    // 発射する角度の広さ
    [SerializeField, Range(0, 360)] private float angleRange;
    public float AngleRange { get { return angleRange; } set { angleRange = value; } }

    // 発射する角度
    [SerializeField, Range(0, 360)] private float shotAngle;
    public float ShotAngle { get { return shotAngle; } set { shotAngle = value; } }

    // 回転弾の時に、1発撃つと動く角度
    [SerializeField, Range(-10, 10)] private float spinAngleShift;
    public float SpinAngleShift { get { return spinAngleShift; } set { spinAngleShift = value; } }
}
