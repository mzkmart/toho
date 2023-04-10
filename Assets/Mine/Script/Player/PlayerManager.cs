using UnityEngine;

//�v���C���[�̃N���X�݌v
public class PlayerManager : MonoBehaviour
{
    protected int _playerLife = 3;
    protected float _moveValue = 0.03f;
    protected float _shotTime = 0.0f;
    protected Vector2 _playerPotision = default;
    protected float _inputMinLimitValue = 0.3f;
    protected Animator _playerAnimator = default;
    protected Rigidbody2D _rigidbody2D = default;

    protected enum PlayerState
    {
        Idle,
        Right,
        Left,
        Damage
    }

    protected PlayerState _playerState = PlayerState.Idle;

    [SerializeField]
    protected GameObject _joystick;
    protected Joystick _joystickScript = default;
    protected float _verticalInputValue = default;
    protected float _horizontalInputValue = default;

    protected PlayerBulletPool _playerBulletPool = default;

    // Unity�̉摜�Ǘ��R���|�[�l���g
    private protected SpriteRenderer spriteRenderer;

    // �v���C���[�̒e���Ǘ�������N���X
    protected PlayerBulletPool playerBulletPool;

    [SerializeField]
    public PlayerShotData _playerShotMode;

    protected void Awake()
    {
        _playerBulletPool = GameObject.FindGameObjectWithTag("PlayerBulletPool").GetComponent<PlayerBulletPool>();
    }
    private protected void PlayerShot(PlayerShotData shotData, BulletSpriteData bulletData, int shotCount, Vector2 vector2)
    {
        // �V���b�g�p�^�[����ǂݍ���Ő����i�p�^�[�����Ƃɒe�����Ⴄ�̂�For���[�v�j
        for (int i = 0; i < shotData.OnceShotBullet; i++)
        {
            Vector2 moveDirection = BulletVectorCalculation.BulletVector(transform.position, shotData, i, shotCount);

            _playerBulletPool.InstBullet(vector2, bulletData, shotData.BulletSpeed, moveDirection);
        }
    }
}
