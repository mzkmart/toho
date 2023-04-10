using UnityEngine;

/// <summary>
/// �e���iScriptableObject�j 
/// </summary>

[CreateAssetMenu(fileName = "BulletSpriteData", menuName = "Data/NewBulletSpriteData")]
public class BulletSpriteData : ScriptableObject
{
    // �e�̉摜�i�J���[�o���G�[�V���������j
    [SerializeField] private Sprite[] _bulletImage;
    public Sprite[] _bulletImages { get { return _bulletImages; } }
}
