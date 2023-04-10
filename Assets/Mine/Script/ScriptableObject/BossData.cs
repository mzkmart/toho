using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �L�����N�^�[�摜�̃��X�g�iScriptableObject�j 
/// </summary>

[CreateAssetMenu(fileName = "BossDates", menuName = "Data/NewBossDate")]
public class BossData : ScriptableObject
{
    // �ړ����Ă��Ȃ����̃L�����N�^�[�摜
    [SerializeField] private Sprite[] bossImageIdle;
    public Sprite[] BossImageIdle { get { return bossImageIdle; } }

    // �E�ړ����̃L�����N�^�[�摜
    [SerializeField] private Sprite[] bossImageRightMove;
    public Sprite[] BossImageRightMove { get { return bossImageRightMove; } }

    // �G�̓����蔻��̔��a
    [SerializeField] private float bossColliderRadius;
    public float BossCollisionRadius { get { return bossColliderRadius; } }

    // �ʒu����
    [SerializeField] private Vector2 offset = default;
    public Vector2 Offset { get { return offset; } }

    // �{�X�f�[�^�̔z�񐶐�
    public List<BossWaveData> bossWaveData = new List<BossWaveData>();
}

[System.Serializable]
public class BossWaveData
{
    // �E�F�[�u��
    [SerializeField] private string name;
    public string Name { get { return name; } }

    // �X�y���J�[�h���ǂ���
    [SerializeField] private bool isSpellCard;
    public bool IsSpellCard { get { return isSpellCard; } }

    // �ړ��p�^�[��
    [SerializeField] private EnemyMovePattern.Pattern bossMovePattern;
    public int BossMovePattern { get { return (int)bossMovePattern; } }

    // �ˌ��p�^�[��
    [SerializeField] private EnemyShotData bossShotData;
    public EnemyShotData BossShotData { get { return bossShotData; } }

    // �̗�
    [SerializeField] private int bossHp;
    public int BossHp { get { return bossHp; } }

    // �E�F�[�u�̎���
    [SerializeField] private int waveTime;
    public int WaveTime { get { return waveTime; } }
}