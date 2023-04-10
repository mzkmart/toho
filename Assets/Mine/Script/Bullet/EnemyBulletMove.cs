using UnityEngine;
/// <summary>
/// �G�̒e�N���X
/// </summary>
public class EnemyBulletMove : BulletBase
{
    // �e�̐i�s����
    private Vector2 moveDirection = default;
    public Vector2 MoveDirection { set { moveDirection = value; } }

    private void FixedUpdate()
    {
        // �e��i�s�����̌����悤�ɂ���
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, moveDirection);

        // �e�����f�[�^��������Ă��ē�����
        this.transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}
