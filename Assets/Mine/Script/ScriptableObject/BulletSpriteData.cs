using UnityEngine;

/// <summary>
/// 弾情報（ScriptableObject） 
/// </summary>

[CreateAssetMenu(fileName = "BulletSpriteData", menuName = "Data/NewBulletSpriteData")]
public class BulletSpriteData : ScriptableObject
{
    // 弾の画像（カラーバリエーションも持つ）
    [SerializeField] private Sprite[] _bulletImage;
    public Sprite[] _bulletImages { get { return _bulletImages; } }
}
