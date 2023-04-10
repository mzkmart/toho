using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// キャラクターステータスのリスト（ScriptableObject） 
/// </summary>

[CreateAssetMenu(fileName = "EnemyWavePattern", menuName = "Data/EnemyWaveData")]
public class EnemyWaveData : ScriptableObject
{
    // エネミー管理のリストを生成
    public List<EnemyData> enemyData = new List<EnemyData>();
}

[System.Serializable]
public class EnemyData
{
    // ウェーブ名
    [SerializeField] private string name;
    public string Name { get { return name; } }

    public enum WaveEnemyType
    {
        Normal,
        MidBoss,
        Boss,
    }

    // 敵の種類
    [SerializeField] private WaveEnemyType enemyType;
    public WaveEnemyType EnemyType { get { return enemyType; } }

    // キャラのデータ（ザコ敵）
    [SerializeField] private EnemySpriteData enemySprite;
    public EnemySpriteData EnemySpriteData { get { return enemySprite; } }

    // キャラのデータ（ボス敵）
    [SerializeField] private BossData bossWave;
    public BossData BossWave { get { return bossWave; } }

    // 出現までの時間(マネージャーで取る)
    [SerializeField] private float appearanceTime;
    public float AppearanceTime { get { return appearanceTime; } }

    // 移動パターン
    [SerializeField] private EnemyMovePattern.Pattern movePattern;
    public int MovePattern { get { return (int)movePattern; } }

    // すばやさ
    [SerializeField] private float speed;
    public float Speed { get { return speed; } }

    // 出現位置のX
    [SerializeField] private float appearancePositionX;
    public float AppearancePositionX { get { return appearancePositionX; } }

    // 出現位置のY
    [SerializeField] private float appearancePositionY;
    public float AppearancePositionY { get { return appearancePositionY; } }

    // 射撃パターン
    [SerializeField] private EnemyShotData shotData;
    public EnemyShotData ShotData { get { return shotData; } }

    // 体力
    [SerializeField] private int hp;
    public int Hp { get { return hp; } }

    // Pのドロップ量
    [SerializeField] private int powerItemDropValue;
    public int PowerItemDropValue { get { return powerItemDropValue; } }

    // 点のドロップ量
    [SerializeField] private int scoreItemDropValue;
    public int ScoreItemDropValue { get { return scoreItemDropValue; } }
}