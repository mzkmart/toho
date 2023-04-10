using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    // 画像描画コンポーネント
    SpriteRenderer spriteRenderer;

    // 弾の速さ
    private protected float speed = default;
    public float Speed { set { speed = value; } }

    private void Start()
    {
        // 画像描画コンポーネントを取得
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
