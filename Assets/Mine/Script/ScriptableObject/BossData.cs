using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// キャラクター画像のリスト（ScriptableObject） 
/// </summary>

[CreateAssetMenu(fileName = "BossDates", menuName = "Data/NewBossDate")]
public class BossData : ScriptableObject
{
    // 移動していない時のキャラクター画像
    [SerializeField] private Sprite[] bossImageIdle;
    public Sprite[] BossImageIdle { get { return bossImageIdle; } }

    // 右移動時のキャラクター画像
    [SerializeField] private Sprite[] bossImageRightMove;
    public Sprite[] BossImageRightMove { get { return bossImageRightMove; } }

    // 敵の当たり判定の半径
    [SerializeField] private float bossColliderRadius;
    public float BossCollisionRadius { get { return bossColliderRadius; } }

    // 位置調整
    [SerializeField] private Vector2 offset = default;
    public Vector2 Offset { get { return offset; } }

    // ボスデータの配列生成
    public List<BossWaveData> bossWaveData = new List<BossWaveData>();
}

[System.Serializable]
public class BossWaveData
{
    // ウェーブ名
    [SerializeField] private string name;
    public string Name { get { return name; } }

    // スペルカードかどうか
    [SerializeField] private bool isSpellCard;
    public bool IsSpellCard { get { return isSpellCard; } }

    // 移動パターン
    [SerializeField] private EnemyMovePattern.Pattern bossMovePattern;
    public int BossMovePattern { get { return (int)bossMovePattern; } }

    // 射撃パターン
    [SerializeField] private EnemyShotData bossShotData;
    public EnemyShotData BossShotData { get { return bossShotData; } }

    // 体力
    [SerializeField] private int bossHp;
    public int BossHp { get { return bossHp; } }

    // ウェーブの時間
    [SerializeField] private int waveTime;
    public int WaveTime { get { return waveTime; } }
}