using UnityEngine;

public class PlayerBulletMove : BulletBase
{
    // �e�̐i�s����
    private Vector2 moveDirection = default;
    public Vector2 MoveDirection { set { moveDirection = value; } }

    private void FixedUpdate()
    {
        // �e�����f�[�^��������Ă��ē�����
        this.transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}