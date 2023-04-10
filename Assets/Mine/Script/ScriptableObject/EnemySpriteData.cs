using UnityEngine;


/// <summary>
/// キャラクター画像のリスト（ScriptableObject） 
/// </summary>

[CreateAssetMenu(fileName = "EnemySpriteData", menuName = "Data/NewEnemySpriteData")]
public class EnemySpriteData : ScriptableObject
{
    // 移動していない時のキャラクター画像
    [SerializeField] private Sprite[] enemyImageIdle;
    public Sprite[] EnemyImageIdle { get { return enemyImageIdle; } }

    // 右移動時のキャラクター画像
    [SerializeField] private Sprite[] enemyImageRightMove;
    public Sprite[] EnemyImageRightMove { get { return enemyImageRightMove; } }

    // 敵の当たり判定の半径
    [SerializeField] private float enemyColliderRadius;
    public float EnemyCollisionRadius { get { return enemyColliderRadius; } }

    // 位置調整
    [SerializeField] private Vector2 offset = default;
    public Vector2 Offset { get { return offset; } }
}
