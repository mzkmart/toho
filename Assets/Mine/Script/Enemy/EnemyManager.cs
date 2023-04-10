using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G�̊Ǘ��E�v�[�����O������N���X
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] _audioClips = null;


    // �����E�v�[�����O����I�u�W�F�N�g
    [SerializeField] private GameObject enemy = default;

    // �����Ɏg�p����f�[�^
    [SerializeField] private List<EnemyWaveData> enemyWaveDatas = default;

    // �I�u�W�F�N�g���Ǘ����铮�I�z��ƁA�擾�p�̃v���p�e�B
    private List<EnemyMove> enemyList = default;
    public List<EnemyMove> EnemyList { get { return enemyList; } }


    // �{�X�̃}�l�[�W���[
    BossManager bossManager;

    // ���݃{�X�Ɛ퓬���Ă��邩
    private bool nowBossBattle = default;
    public bool NowBossBattle { set { nowBossBattle = value; } }

    // �G�I�u�W�F�N�g���S��false��
    private bool isAllEnemyLoadAndFalse = default;
    public bool IsAllEnemyLoadAndFalse { get { return isAllEnemyLoadAndFalse; } }

    // �G�����ׂ̈̎��Ԍv���p�^�C�}�[
    private float elapsedTimer = default;

    // ���ݓǂݍ���ł���ScriptableObject�f�[�^�̔ԍ�
    private int nowLoadingEnemyWaveDate = default;

    // �G�Ǘ��p�̃f�[�^��錾����\����
    private struct EnemyInformation
    {
        public bool isBoss;
        public float appearanceTime;
        public int waveDateKey;
        public int enemyDateKey;
    }

    // �G�Ǘ��p�̃f�[�^��List
    private List<EnemyInformation> enemyInformationList = default;

    // �v���C�O�ɂ��炩���ߐ������Ă����I�u�W�F�N�g�̐�
    [SerializeField] private int beforeCreateObjects = default;

    private void Awake()
    {
        // ���X�g����������
        enemyList = new List<EnemyMove>();
        enemyInformationList = new List<EnemyInformation>();

        // ���炩���߃v�[���p�I�u�W�F�N�g�𐶐�
        for (int i = 0; i < beforeCreateObjects; i++)
        {
            CreateNewObject();
        }

        // �G�Ǘ��p�̃f�[�^�̃��X�g���쐬
        CreateEnemyInfoList();
    }

    private void Start()
    {
        // �{�X�}�l�[�W���[
        bossManager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<BossManager>();

        // �N���A�p�̃N���X���擾
        //clearSelect = GameObject.FindGameObjectWithTag("GameClearManager").GetComponent<GameClearSelect>();
    }

    private void FixedUpdate()
    {
        // �{�X�Ɛ퓬�� or �S�ẴE�F�[�u��ǂ񂾂��A�G�I�u�W�F�N�g���S��False�Ȃ珈�����I��
        if (nowBossBattle || isAllEnemyLoadAndFalse)
        {
            return;
        }

        if (enemyInformationList.Count <= 0)
        {
            if (EnemyActiveFalseCheck())
            {
                // �N���A�}�l�[�W���[�ɁA�G�I�u�W�F�N�g���S��false�ȃt���O��n��
                isAllEnemyLoadAndFalse = true;
            }

            return;
        }

        // �E�F�[�u�ɉ������G�̐�������
        InstantiateEnemyObject();
    }

    /// <summary>
    /// �V�����G�I�u�W�F�N�g�𐶐�
    /// </summary>
    private void CreateNewObject()
    {
        // �v���C��ʊO�ɐ���
        GameObject enemyObject = Instantiate(enemy, new Vector2(30, 30), Quaternion.identity);

        // �G�����̃I�u�W�F�N�g�̎q�ɐݒ�
        enemyObject.transform.parent = this.transform;

        // ���������I�u�W�F�N�g�𓮓I�z��ɕۑ�
        enemyList.Add(enemyObject.GetComponent<EnemyMove>());

        // �I�u�W�F�N�g���\����
        enemyObject.SetActive(false);

        // �I�u�W�F�N�g�̖��O��ݒ�
        enemyObject.name = "Enemy (" + enemyList.Count + ")";
    }

    /// <summary>
    /// �G�Ǘ��p�̃f�[�^�̃��X�g���쐬���郁�\�b�h�iStart�ň��̂݁j
    /// </summary>
    private void CreateEnemyInfoList()
    {
        // �f�[�^�𕪂��邽�߂̉����X�g���`
        List<EnemyInformation> addDates = default;
        List<EnemyData> enemyDates = default;

        for (int i = 0; i < enemyWaveDatas.Count; i++)
        {
            // �g�p����Ă���ScriptableObject�f�[�^���̃��X�g���C���X�^���X��
            enemyDates = enemyWaveDatas[i].enemyData;

            if (enemyDates == null || enemyDates.Count <= 0) // null���v�f����0�Ȃ��΂�
            {
                continue;
            }

            // ������f�[�^��������
            addDates = new List<EnemyInformation>();

            for (int j = 0; j < enemyDates.Count; j++)
            {
                // �f�[�^����U�����Ċi�[
                if (enemyDates[j].EnemyType == EnemyData.WaveEnemyType.Normal)
                {
                    addDates.Add(new EnemyInformation
                    {
                        isBoss = false,
                        appearanceTime = enemyDates[j].AppearanceTime,
                        waveDateKey = i,
                        enemyDateKey = j
                    }); ;
                }
                else
                {
                    addDates.Add(new EnemyInformation
                    {
                        isBoss = true,
                        appearanceTime = enemyDates[j].AppearanceTime,
                        waveDateKey = i,
                        enemyDateKey = j
                    }); ;
                }
            }

            // �������f�[�^���ŁA�o�����Ԃ̍~���ŕ��בւ����Č���
            addDates.Sort((a, b) => (int)(a.appearanceTime * 100) - (int)(b.appearanceTime * 100));
            enemyInformationList.AddRange(addDates);
        }
    }

    /// <summary>
    /// �G�I�u�W�F�N�g���S��False�ɂȂ��Ă��邩�̊m�F
    /// </summary>
    /// <returns>�G���S��False�ɂȂ��Ă��邩�̐^�U</returns>
    private bool EnemyActiveFalseCheck()
    {
        // �A�N�e�B�u�ȃI�u�W�F�N�g������Ȃ珈�����I��
        foreach (EnemyMove enemyObject in enemyList)
        {
            if (enemyObject.gameObject.activeSelf)
            {
                return false;
            }
            else
            {
                continue;
            }
        }
        return true;
    }

    /// <summary>
    /// �G�̐������Ǘ����郁�\�b�h
    /// </summary>
    private void InstantiateEnemyObject()
    {
        // �ǂݍ���ł���f�[�^���؂�ւ������^�C�}�[�����Z�b�g
        if (enemyInformationList[0].waveDateKey != nowLoadingEnemyWaveDate)
        {
            nowLoadingEnemyWaveDate = enemyInformationList[0].waveDateKey;
            elapsedTimer = 0;
        }

        // �G�����p�^�C�}�[�̃J�E���g
        elapsedTimer += Time.deltaTime;

        // �^�C�}�[���G�������X�g�̐擪�̐������Ԃ������Ă��Ȃ��Ȃ�I��
        if (enemyInformationList[0].appearanceTime >= elapsedTimer)
        {
            return;
        }

        // ����������G�̎�ނ𔻕ʂ��Đ���
        if (enemyInformationList[0].isBoss)
        {
            if (EnemyActiveFalseCheck())
            {
                // �퓬�t���O��True��
                nowBossBattle = true;

                InstBoss();
                enemyInformationList.RemoveAt(0);
            }
        }
        else
        {
            InstNormalEnemy();
            enemyInformationList.RemoveAt(0);
        }
    }

    /// <summary>
    /// �U�R�G�̐������\�b�h
    /// </summary>
    public void InstNormalEnemy()
    {
        // ��A�N�e�B�u�ȃI�u�W�F�N�g��T���āA����Ȃ�Ώ���n���ăA�N�e�B�u�ɂ���
        foreach (EnemyMove enemyObject in enemyList)
        {
            if (enemyObject.gameObject.activeSelf)
            {
                continue;
            }

            // �G�l�~�[���A�N�e�B�u�ɂ��ď���n��
            enemyObject.OnEnableInitLoad(enemyWaveDatas[enemyInformationList[0].waveDateKey], enemyInformationList[0].enemyDateKey);
            enemyObject.transform.gameObject.SetActive(true);
            return;
        }

        // ��������Ȃ������ꍇ�ɐV�K����
        CreateNewObject();
        InstNormalEnemy();
    }

    /// <summary>
    /// �{�X�̐���
    /// </summary>
    private void InstBoss()
    {
        _audioSource.clip = _audioClips[1];
        _audioSource.Play();

        // �{�X�}�l�[�W���[�Ƀ{�X�̏���n���Đ������Ă��炤
        bossManager.ActiveBoss(enemyWaveDatas[enemyInformationList[0].waveDateKey].
        enemyData[enemyInformationList[0].enemyDateKey].BossWave);
    }
}