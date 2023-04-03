using UnityEngine;

/// <summary>
/// 弾情報（ScriptableObject） 
/// </summary>

[CreateAssetMenu(fileName = "BulletSpliteData", menuName = "Data/NewBulletSpliteData")]
public class BulletSpliteData : ScriptableObject
{
    // 弾の画像（カラーバリエーションも持つ）
    [SerializeField] private Sprite[] _bulletImage;
    public Sprite[] _bulletImages { get { return _bulletImages; } }
}
