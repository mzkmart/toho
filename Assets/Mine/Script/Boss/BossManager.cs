using UnityEngine;
/// <summary>
/// ボスの管理をするクラス
/// </summary>
public class BossManager : MonoBehaviour
{
    // 生成・プーリングするオブジェクト
    [SerializeField] private GameObject bossObjectPrefab = default;

    // 生成されたオブジェクト
    private GameObject bossObject = default;

    private void Awake()
    {
        // プレイ画面外に生成 格納
        bossObject = Instantiate(bossObjectPrefab, new Vector2(30, 30), Quaternion.identity);

        // プレイヤーの弾を特定のオブジェクトの子に設定
        bossObject.transform.parent = this.transform;

        // オブジェクトを非表示に
        bossObject.SetActive(false);

        // オブジェクトの名前を設定
        bossObject.name = "BossObject";
    }

    /// <summary>
    /// 敵の生成メソッド
    /// </summary>
    public void ActiveBoss(BossData newBossData)
    {
        // ボスをアクティブにして情報を渡す
        bossObject.SetActive(true);
        bossObject.GetComponent<BossEnemy>().ActiveInitLoad(newBossData);
    }
}