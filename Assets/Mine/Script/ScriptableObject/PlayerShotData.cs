using UnityEditor;
using UnityEngine;

/// <summary>
/// “G‚Ì’e‚ÌËŒ‚î•ñiScriptableObjectj 
/// </summary>

[CreateAssetMenu(fileName = "PlayerShotDate", menuName = "Data/NewPlayerShotDate")]
public class PlayerShotData : ScriptableObject
{
    // ËŒ‚ƒ^ƒCƒv‚Æ’e‚Ì‹O“¹
    public PlayerBulletVectorCalculation.ShotType shotType;
    public PlayerBulletVectorCalculation.AngleType angleType;

    // ’e‘¬
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }

    // ’e‚Ìí—Ş
    [SerializeField] private PlayerBulletData bulletData;
    public PlayerBulletData BulletData { get { return bulletData; } set { bulletData = value; } }

    // ’e‚ÌF
    [SerializeField] private int bulletColorType;
    public int BulletColorType { get { return bulletColorType; } set { bulletColorType = value; } }

    // ”­ËŠÔŠuŠÔ
    [SerializeField] private float shotIntervalTime;
    public float ShotIntervalTime { get { return shotIntervalTime; } set { shotIntervalTime = value; } }

    // N-Way’e‚Ì—ñ”
    [SerializeField] private int onceShotBullet;
    public int OnceShotBullet { get { return onceShotBullet; } set { onceShotBullet = value; } }

    // ”­Ë‚·‚éŠp“x‚ÌL‚³
    [SerializeField, Range(0, 360)] private float angleRange;
    public float AngleRange { get { return angleRange; } set { angleRange = value; } }

    // ”­Ë‚·‚éŠp“x
    [SerializeField, Range(0, 360)] private float shotAngle;
    public float ShotAngle { get { return shotAngle; } set { shotAngle = value; } }

    // ‰ñ“]’e‚Ì‚ÉA1”­Œ‚‚Â‚Æ“®‚­Šp“x
    [SerializeField, Range(-10, 10)] private float spinAngleShift;
    public float SpinAngleShift { get { return spinAngleShift; } set { spinAngleShift = value; } }
}