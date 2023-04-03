using UnityEngine;

/// <summary>
/// �e���iScriptableObject�j 
/// </summary>

[CreateAssetMenu(fileName = "BulletSpliteData", menuName = "Data/NewBulletSpliteData")]
public class BulletSpliteData : ScriptableObject
{
    // �e�̉摜�i�J���[�o���G�[�V���������j
    [SerializeField] private Sprite[] _bulletImage;
    public Sprite[] _bulletImages { get { return _bulletImages; } }
}
