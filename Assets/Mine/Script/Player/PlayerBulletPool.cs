using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    //オブジェクトプールのオブジェクト(自身)のTrabform
    private Transform _poolTransform;
    //生成する敵オブジェクトのプレハブ
    [SerializeField] GameObject _playerBulletPrefab = null;
    
    private float _bulletRandamRotation = 0.0f;

    private GameObject _player = default;

    void Start()
    {
        //オブジェクトプールのTransformを取得
        _poolTransform = this.transform;

        _player = GameObject.FindWithTag("Player");
    }

    //敵を生成するかtrueにするか
    public void InstBullet(Vector3 pos, BulletSpriteData enemyBulletData, float bulletSpeed, Vector2 moveDirection)
    {
        _bulletRandamRotation = Random.Range(-19.0f, 18.0f);
        Quaternion a = Quaternion.Euler(0, 0, _bulletRandamRotation);

        PlayerBulletMove playerBulletMove;

        //アクティブでないオブジェクトを子の中から探索
        foreach (Transform t in _poolTransform)
        {
            if (!t.gameObject.activeSelf)
            {
                // 弾のポジション・情報を設定
                t.transform.SetPositionAndRotation(pos, a);

                t.transform.gameObject.SetActive(true);

                // 各種パラメータを取得
                playerBulletMove = t.GetComponent<PlayerBulletMove>();
                playerBulletMove.Speed = bulletSpeed;
                playerBulletMove.MoveDirection = moveDirection;
                return;
            }
        }

        GameObject newBullet = Instantiate(_playerBulletPrefab, pos, a, _poolTransform);
        newBullet.transform.parent = this.transform;
        playerBulletMove = newBullet.GetComponent<PlayerBulletMove>();
        // 各種パラメータを取得
        playerBulletMove.Speed = bulletSpeed;
        playerBulletMove.MoveDirection = moveDirection;
    }

}
