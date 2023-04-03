using UnityEngine;

/// <summary>
/// �v���C���[�̒e���iScriptableObject�j 
/// </summary>

[CreateAssetMenu(fileName = "PlayerBulletData", menuName = "Data/NewPlayerBulletData")]
public class PlayerBulletData : ScriptableObject
{
    // �e�̉摜�i�J���[�o���G�[�V���������j
    [SerializeField] private Sprite[] bulletImage;
    public Sprite[] BulletImage { get { return bulletImage; } }
}
