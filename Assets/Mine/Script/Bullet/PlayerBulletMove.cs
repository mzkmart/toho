using UnityEngine;

public class PlayerBulletMove : BulletBase
{
    // 弾の進行方向
    private Vector2 moveDirection = default;
    public Vector2 MoveDirection { set { moveDirection = value; } }

    private void FixedUpdate()
    {
        // 弾挙動データからもってきて動かす
        this.transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}