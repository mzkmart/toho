using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �L�����N�^�[�X�e�[�^�X�̃��X�g�iScriptableObject�j 
/// </summary>

[CreateAssetMenu(fileName = "EnemyWavePattern", menuName = "Data/EnemyWaveData")]
public class EnemyWaveData : ScriptableObject
{
    // �G�l�~�[�Ǘ��̃��X�g�𐶐�
    public List<EnemyData> enemyData = new List<EnemyData>();
}

[System.Serializable]
public class EnemyData
{
    // �E�F�[�u��
    [SerializeField] private string name;
    public string Name { get { return name; } }

    public enum WaveEnemyType
    {
        Normal,
        MidBoss,
        Boss,
    }

    // �G�̎��
    [SerializeField] private WaveEnemyType enemyType;
    public WaveEnemyType EnemyType { get { return enemyType; } }

    // �L�����̃f�[�^�i�U�R�G�j
    [SerializeField] private EnemySpriteData enemySprite;
    public EnemySpriteData EnemySpriteData { get { return enemySprite; } }

    // �L�����̃f�[�^�i�{�X�G�j
    [SerializeField] private BossData bossWave;
    public BossData BossWave { get { return bossWave; } }

    // �o���܂ł̎���(�}�l�[�W���[�Ŏ��)
    [SerializeField] private float appearanceTime;
    public float AppearanceTime { get { return appearanceTime; } }

    // �ړ��p�^�[��
    [SerializeField] private EnemyMovePattern.Pattern movePattern;
    public int MovePattern { get { return (int)movePattern; } }

    // ���΂₳
    [SerializeField] private float speed;
    public float Speed { get { return speed; } }

    // �o���ʒu��X
    [SerializeField] private float appearancePositionX;
    public float AppearancePositionX { get { return appearancePositionX; } }

    // �o���ʒu��Y
    [SerializeField] private float appearancePositionY;
    public float AppearancePositionY { get { return appearancePositionY; } }

    // �ˌ��p�^�[��
    [SerializeField] private EnemyShotData shotData;
    public EnemyShotData ShotData { get { return shotData; } }

    // �̗�
    [SerializeField] private int hp;
    public int Hp { get { return hp; } }

    // P�̃h���b�v��
    [SerializeField] private int powerItemDropValue;
    public int PowerItemDropValue { get { return powerItemDropValue; } }

    // �_�̃h���b�v��
    [SerializeField] private int scoreItemDropValue;
    public int ScoreItemDropValue { get { return scoreItemDropValue; } }
}