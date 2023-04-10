using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    //�I�u�W�F�N�g�v�[���̃I�u�W�F�N�g(���g)��Trabform
    private Transform _poolTransform;
    //��������G�I�u�W�F�N�g�̃v���n�u
    [SerializeField] GameObject _playerBulletPrefab = null;

    void Start()
    {
        //�I�u�W�F�N�g�v�[����Transform���擾
        _poolTransform = this.transform;

        for (int i = 0; i < 100; i++)
        {
            GameObject newBullet = Instantiate(_playerBulletPrefab, new Vector2(30, 30), Quaternion.identity, _poolTransform);
            newBullet.transform.parent = this.transform;
            newBullet.SetActive(false);
        }
    }

    //�G�𐶐����邩true�ɂ��邩
    public void InstBullet(Vector3 pos, BulletSpriteData enemyBulletData, float bulletSpeed, Vector2 moveDirection)
    {
        EnemyBulletMove EnemyBulletMove;

        //�A�N�e�B�u�łȂ��I�u�W�F�N�g���q�̒�����T��
        foreach (Transform t in _poolTransform)
        {
            if (!t.gameObject.activeSelf)
            {
                // �e�̃|�W�V�����E����ݒ�
                t.transform.SetPositionAndRotation(pos, Quaternion.identity);
                t.transform.gameObject.SetActive(true);

                // �e��p�����[�^���擾
                EnemyBulletMove = t.GetComponent<EnemyBulletMove>();
                EnemyBulletMove.Speed = bulletSpeed;
                EnemyBulletMove.MoveDirection = moveDirection;

                // �摜��ݒ�
                SpriteRenderer spriteRenderer = t.GetComponent<SpriteRenderer>();

                //spriteRenderer.sprite = enemyBulletData._bulletImages[0];
                return;
            }
        }

        GameObject newBullet = Instantiate(_playerBulletPrefab, pos, Quaternion.identity, _poolTransform);
        newBullet.transform.parent = this.transform;
        EnemyBulletMove = newBullet.GetComponent<EnemyBulletMove>();
        // �e��p�����[�^���擾
        EnemyBulletMove.Speed = bulletSpeed;
        EnemyBulletMove.MoveDirection = moveDirection;
    }

}
