using UnityEngine;
/// <summary>
/// 敵の弾クラス
/// </summary>
public class EnemyBulletMove : BulletBase
{
    // 弾の進行方向
    private Vector2 moveDirection = default;
    public Vector2 MoveDirection { set { moveDirection = value; } }

    private void FixedUpdate()
    {
        // 弾を進行方向の向くようにする
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, moveDirection);

        // 弾挙動データからもってきて動かす
        this.transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}
