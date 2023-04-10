using UnityEngine;


/// <summary>
/// �L�����N�^�[�摜�̃��X�g�iScriptableObject�j 
/// </summary>

[CreateAssetMenu(fileName = "EnemySpriteData", menuName = "Data/NewEnemySpriteData")]
public class EnemySpriteData : ScriptableObject
{
    // �ړ����Ă��Ȃ����̃L�����N�^�[�摜
    [SerializeField] private Sprite[] enemyImageIdle;
    public Sprite[] EnemyImageIdle { get { return enemyImageIdle; } }

    // �E�ړ����̃L�����N�^�[�摜
    [SerializeField] private Sprite[] enemyImageRightMove;
    public Sprite[] EnemyImageRightMove { get { return enemyImageRightMove; } }

    // �G�̓����蔻��̔��a
    [SerializeField] private float enemyColliderRadius;
    public float EnemyCollisionRadius { get { return enemyColliderRadius; } }

    // �ʒu����
    [SerializeField] private Vector2 offset = default;
    public Vector2 Offset { get { return offset; } }
}
