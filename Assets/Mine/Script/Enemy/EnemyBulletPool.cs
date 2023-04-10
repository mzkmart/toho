using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    //オブジェクトプールのオブジェクト(自身)のTrabform
    private Transform _poolTransform;
    //生成する敵オブジェクトのプレハブ
    [SerializeField] GameObject _playerBulletPrefab = null;

    void Start()
    {
        //オブジェクトプールのTransformを取得
        _poolTransform = this.transform;

        for (int i = 0; i < 100; i++)
        {
            GameObject newBullet = Instantiate(_playerBulletPrefab, new Vector2(30, 30), Quaternion.identity, _poolTransform);
            newBullet.transform.parent = this.transform;
            newBullet.SetActive(false);
        }
    }

    //敵を生成するかtrueにするか
    public void InstBullet(Vector3 pos, BulletSpriteData enemyBulletData, float bulletSpeed, Vector2 moveDirection)
    {
        EnemyBulletMove EnemyBulletMove;

        //アクティブでないオブジェクトを子の中から探索
        foreach (Transform t in _poolTransform)
        {
            if (!t.gameObject.activeSelf)
            {
                // 弾のポジション・情報を設定
                t.transform.SetPositionAndRotation(pos, Quaternion.identity);
                t.transform.gameObject.SetActive(true);

                // 各種パラメータを取得
                EnemyBulletMove = t.GetComponent<EnemyBulletMove>();
                EnemyBulletMove.Speed = bulletSpeed;
                EnemyBulletMove.MoveDirection = moveDirection;

                // 画像を設定
                SpriteRenderer spriteRenderer = t.GetComponent<SpriteRenderer>();

                //spriteRenderer.sprite = enemyBulletData._bulletImages[0];
                return;
            }
        }

        GameObject newBullet = Instantiate(_playerBulletPrefab, pos, Quaternion.identity, _poolTransform);
        newBullet.transform.parent = this.transform;
        EnemyBulletMove = newBullet.GetComponent<EnemyBulletMove>();
        // 各種パラメータを取得
        EnemyBulletMove.Speed = bulletSpeed;
        EnemyBulletMove.MoveDirection = moveDirection;
    }

}
