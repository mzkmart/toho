using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    //�I�u�W�F�N�g�v�[���̃I�u�W�F�N�g(���g)��Trabform
    private Transform _poolTransform;
    //��������G�I�u�W�F�N�g�̃v���n�u
    [SerializeField] GameObject _playerBulletPrefab = null;
    
    private float _bulletRandamRotation = 0.0f;

    private GameObject _player = default;

    void Start()
    {
        //�I�u�W�F�N�g�v�[����Transform���擾
        _poolTransform = this.transform;

        _player = GameObject.FindWithTag("Player");
    }

    //�G�𐶐����邩true�ɂ��邩
    public void InstBullet(Vector3 pos, BulletSpriteData enemyBulletData, float bulletSpeed, Vector2 moveDirection)
    {
        _bulletRandamRotation = Random.Range(-19.0f, 18.0f);
        Quaternion a = Quaternion.Euler(0, 0, _bulletRandamRotation);

        PlayerBulletMove playerBulletMove;

        //�A�N�e�B�u�łȂ��I�u�W�F�N�g���q�̒�����T��
        foreach (Transform t in _poolTransform)
        {
            if (!t.gameObject.activeSelf)
            {
                // �e�̃|�W�V�����E����ݒ�
                t.transform.SetPositionAndRotation(pos, a);

                t.transform.gameObject.SetActive(true);

                // �e��p�����[�^���擾
                playerBulletMove = t.GetComponent<PlayerBulletMove>();
                playerBulletMove.Speed = bulletSpeed;
                playerBulletMove.MoveDirection = moveDirection;
                return;
            }
        }

        GameObject newBullet = Instantiate(_playerBulletPrefab, pos, a, _poolTransform);
        newBullet.transform.parent = this.transform;
        playerBulletMove = newBullet.GetComponent<PlayerBulletMove>();
        // �e��p�����[�^���擾
        playerBulletMove.Speed = bulletSpeed;
        playerBulletMove.MoveDirection = moveDirection;
    }

}
