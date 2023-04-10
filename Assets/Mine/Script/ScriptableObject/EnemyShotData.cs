using UnityEngine;


/// <summary>
/// �G�̒e�̎ˌ����iScriptableObject�j 
/// </summary>

[CreateAssetMenu(fileName = "EnemyShotDate", menuName = "Data/NewEnemyShotDate")]
public class EnemyShotData : ScriptableObject
{
    // �ˌ��^�C�v�ƒe�̋O��
    public BulletVectorCalculation.ShotType shotType;
    public BulletVectorCalculation.AngleType angleType;

    // �e��
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }

    // �e�̎��
    [SerializeField] private BulletSpriteData bulletData;
    public BulletSpriteData BulletData { get { return bulletData; } set { bulletData = value; } }

    // �e�̐F
    [SerializeField] private int bulletColorType;
    public int BulletColorType { get { return bulletColorType; } set { bulletColorType = value; } }

    // �ˌ����J�n����܂ł̎���
    [SerializeField] private float shotStartTime;
    public float ShotStartTime { get { return shotStartTime; } set { shotStartTime = value; } }

    // �ˌ����I������܂ł̎���
    [SerializeField] private float shotEndTime;
    public float ShotEndTime { get { return shotEndTime; } set { shotEndTime = value; } }

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
