using UnityEngine;

/// <summary>
/// GÌeÌËîñiScriptableObjectj 
/// </summary>

[CreateAssetMenu(fileName = "PlayerShotDate", menuName = "Data/NewPlayerShotDate")]
public class PlayerShotData : ScriptableObject
{
    // Ë^CvÆeÌO¹
    public BulletVectorCalculation.ShotType shotType;
    public BulletVectorCalculation.AngleType angleType;

    // e¬
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }

    // eÌíÞ
    [SerializeField] private BulletSpriteData bulletData;
    public BulletSpriteData BulletData { get { return bulletData; } set { bulletData = value; } }

    // eÌF
    [SerializeField] private int bulletColorType;
    public int BulletColorType { get { return bulletColorType; } set { bulletColorType = value; } }

    // ­ËÔuÔ
    [SerializeField] private float shotIntervalTime;
    public float ShotIntervalTime { get { return shotIntervalTime; } set { shotIntervalTime = value; } }

    // N-WayeÌñ
    [SerializeField] private int onceShotBullet;
    public int OnceShotBullet { get { return onceShotBullet; } set { onceShotBullet = value; } }

    // ­Ë·épxÌL³
    [SerializeField, Range(0, 360)] private float angleRange;
    public float AngleRange { get { return angleRange; } set { angleRange = value; } }

    // ­Ë·épx
    [SerializeField, Range(0, 360)] private float shotAngle;
    public float ShotAngle { get { return shotAngle; } set { shotAngle = value; } }

    // ñ]eÌÉA1­ÂÆ®­px
    [SerializeField, Range(-10, 10)] private float spinAngleShift;
    public float SpinAngleShift { get { return spinAngleShift; } set { spinAngleShift = value; } }
}