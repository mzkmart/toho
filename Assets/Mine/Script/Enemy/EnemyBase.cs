using UnityEngine;

/// <summary>
/// �G�̃N���X
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    // �ʒu����
    private protected Vector2 offset = default;
    public Vector2 Offset { get { return offset; } }

    // ������ԓ��ł̈ʒu���̔ԍ�
    private protected int dividedAreaNumber = default;
    public int DividedAreaNumber { get { return dividedAreaNumber; } }

    // HP
    private protected int hp = default;

    // �s���p�^�[��
    private protected int movePattern = default;

    // �ړ����x
    private protected float speed = default;

    // �o������̎��Ԃ��v������ϐ�
    private protected float appeardTime = default;

    // ���e���˂̑҂�����
    private protected float bulletShotInterval = default;

    // �ˌ��p�^�[��
    [SerializeField]
    private protected EnemyShotData shotData;

    // �e�f�[�^
    [SerializeField]
    private protected BulletSpriteData enemyBulletData;

    // �e��
    private protected float bulletSpeed = default;

    // �e�̐F
    private protected int bulletColor = default;

    // �ˌ�����������s��ꂽ��
    private protected int shotUpdateCount = default;

    // �A�j���[�V�����p�̒萔�E�ϐ��Q
    #region Animation

    // �摜�f�[�^�z�������
    private protected Sprite[] idleImagePattern = default;
    private protected Sprite[] rightImagePattern = default;

    // ���܂ǂ���̉摜�f�[�^�z���ǂ�ł��邩
    public enum SpriteArray
    {
        idle,
        right,
    }

    private protected SpriteArray loadSpriteArray = default;

    // �摜�؂�ւ��̑҂�����
    private const float IMAGE_CHANGE_INTERVAL = 0.4f;

    // �؂�ւ��Ԋu���v�邽�߂̕ϐ�
    private float imageCountTime = default;

    // �����Ԗڂ̉摜�Ȃ̂����i�[����ϐ�
    private int imageNumber = default;
    #endregion

    protected EnemyBulletPool _enemyBulletPool = default;

    protected bool _isShot = default;

    private void Awake()
    {
        // �ǂݍ���ł���摜�f�[�^�z���������
        loadSpriteArray = SpriteArray.idle;

        _enemyBulletPool = GameObject.FindGameObjectWithTag("EnemyBulletPool").GetComponent<EnemyBulletPool>();
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// �G�̎ˌ�
    /// </summary>
    private protected void EnemyShot(EnemyShotData shotData, BulletSpriteData bulletData, int shotCount)
    {
        // �V���b�g�p�^�[����ǂݍ���Ő����i�p�^�[�����Ƃɒe�����Ⴄ�̂�For���[�v�j
        for (int i = 0; i < shotData.OnceShotBullet; i++)
        {
            Vector2 moveDirection = BulletVectorCalculation.BulletVector(transform.position, shotData, i, shotCount);

            _enemyBulletPool.InstBullet(this.transform.position, bulletData, shotData.BulletSpeed, moveDirection);
        }
    }

    private protected void EnemyMove(int movePattern, float moveSpeed, float appeardTime)
    {
        // �����f�[�^���瓮����ǂ�Ŕ��f
        (float addPositionX, float addPositionY) = EnemyMovePattern.MovePatternLoad(transform, appeardTime, movePattern);

        this.transform.position = new Vector3(this.transform.position.x + (addPositionX * moveSpeed),
            this.transform.position.y + (addPositionY * moveSpeed));

        // �A�j���[�V�����̔��f
        // �摜�f�[�^�������Ȃ�I��
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