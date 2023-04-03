using UnityEditor;
using UnityEngine;

/// <summary>
/// �G�̒e�̎ˌ����iScriptableObject�j 
/// </summary>

[CreateAssetMenu(fileName = "PlayerShotDate", menuName = "Data/NewPlayerShotDate")]
public class PlayerShotData : ScriptableObject
{
    // �ˌ��^�C�v�ƒe�̋O��
    public PlayerBulletVectorCalculation.ShotType shotType;
    public PlayerBulletVectorCalculation.AngleType angleType;

    // �e��
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }

    // �e�̎��
    [SerializeField] private PlayerBulletData bulletData;
    public PlayerBulletData BulletData { get { return bulletData; } set { bulletData = value; } }

    // �e�̐F
    [SerializeField] private int bulletColorType;
    public int BulletColorType { get { return bulletColorType; } set { bulletColorType = value; } }

    // ���ˊԊu����
    [SerializeField] private float shotIntervalTime;
    public float ShotIntervalTime { get { return shotIntervalTime; } set { shotIntervalTime = value; } }

    // N-Way�e�̗�
    [SerializeField] private int onceShotBullet;
    public int OnceShotBullet { get { return onceShotBullet; } set { onceShotBullet = value; } }

    // ���˂���p�x�̍L��
    [SerializeField, Range(0, 360)] private float angleRange;
    public float AngleRange { get { return angleRange; } set { angleRange = value; } }

    // ���˂���p�x
    [SerializeField, Range(0, 360)] private float shotAngle;
    public float ShotAngle { get { return shotAngle; } set { shotAngle = value; } }

    // ��]�e�̎��ɁA1�����Ɠ����p�x
    [SerializeField, Range(-10, 10)] private float spinAngleShift;
    public float SpinAngleShift { get { return spinAngleShift; } set { spinAngleShift = value; } }
}