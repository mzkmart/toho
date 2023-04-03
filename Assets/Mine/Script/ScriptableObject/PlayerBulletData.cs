using UnityEngine;

/// <summary>
/// プレイヤーの弾情報（ScriptableObject） 
/// </summary>

[CreateAssetMenu(fileName = "PlayerBulletData", menuName = "Data/NewPlayerBulletData")]
public class PlayerBulletData : ScriptableObject
{
    // 弾の画像（カラーバリエーションも持つ）
    [SerializeField] private Sprite[] bulletImage;
    public Sprite[] BulletImage { get { return bulletImage; } }
}
