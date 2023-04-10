using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    // �摜�`��R���|�[�l���g
    SpriteRenderer spriteRenderer;

    // �e�̑���
    private protected float speed = default;
    public float Speed { set { speed = value; } }

    private void Start()
    {
        // �摜�`��R���|�[�l���g���擾
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
