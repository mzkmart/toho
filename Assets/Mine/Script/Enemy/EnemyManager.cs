using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の管理・プーリングをするクラス
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] _audioClips = null;


    // 生成・プーリングするオブジェクト
    [SerializeField] private GameObject enemy = default;

    // 生成に使用するデータ
    [SerializeField] private List<EnemyWaveData> enemyWaveDatas = default;

    // オブジェクトを管理する動的配列と、取得用のプロパティ
    private List<EnemyMove> enemyList = default;
    public List<EnemyMove> EnemyList { get { return enemyList; } }


    // ボスのマネージャー
    BossManager bossManager;

    // 現在ボスと戦闘しているか
    private bool nowBossBattle = default;
    public bool NowBossBattle { set { nowBossBattle = value; } }

    // 敵オブジェクトが全てfalseか
    private bool isAllEnemyLoadAndFalse = default;
    public bool IsAllEnemyLoadAndFalse { get { return isAllEnemyLoadAndFalse; } }

    // 敵生成の為の時間計測用タイマー
    private float elapsedTimer = default;

    // 現在読み込んでいるScriptableObjectデータの番号
    private int nowLoadingEnemyWaveDate = default;

    // 敵管理用のデータを宣言する構造体
    private struct EnemyInformation
    {
        public bool isBoss;
        public float appearanceTime;
        public int waveDateKey;
        public int enemyDateKey;
    }

    // 敵管理用のデータのList
    private List<EnemyInformation> enemyInformationList = default;

    // プレイ前にあらかじめ生成しておくオブジェクトの数
    [SerializeField] private int beforeCreateObjects = default;

    private void Awake()
    {
        // リストを初期生成
        enemyList = new List<EnemyMove>();
        enemyInformationList = new List<EnemyInformation>();

        // あらかじめプール用オブジェクトを生成
        for (int i = 0; i < beforeCreateObjects; i++)
        {
            CreateNewObject();
        }

        // 敵管理用のデータのリストを作成
        CreateEnemyInfoList();
    }

    private void Start()
    {
        // ボスマネージャー
        bossManager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<BossManager>();

        // クリア用のクラスを取得
        //clearSelect = GameObject.FindGameObjectWithTag("GameClearManager").GetComponent<GameClearSelect>();
    }

    private void FixedUpdate()
    {
        // ボスと戦闘中 or 全てのウェーブを読んだかつ、敵オブジェクトが全てFalseなら処理を終了
        if (nowBossBattle || isAllEnemyLoadAndFalse)
        {
            return;
        }

        if (enemyInformationList.Count <= 0)
        {
            if (EnemyActiveFalseCheck())
            {
                // クリアマネージャーに、敵オブジェクトが全てfalseなフラグを渡す
                isAllEnemyLoadAndFalse = true;
            }

            return;
        }

        // ウェーブに応じた敵の生成処理
        InstantiateEnemyObject();
    }

    /// <summary>
    /// 新しい敵オブジェクトを生成
    /// </summary>
    private void CreateNewObject()
    {
        // プレイ画面外に生成
        GameObject enemyObject = Instantiate(enemy, new Vector2(30, 30), Quaternion.identity);

        // 敵を特定のオブジェクトの子に設定
        enemyObject.transform.parent = this.transform;

        // 生成したオブジェクトを動的配列に保存
        enemyList.Add(enemyObject.GetComponent<EnemyMove>());

        // オブジェクトを非表示に
        enemyObject.SetActive(false);

        // オブジェクトの名前を設定
        enemyObject.name = "Enemy (" + enemyList.Count + ")";
    }

    /// <summary>
    /// 敵管理用のデータのリストを作成するメソッド（Startで一回のみ）
    /// </summary>
    private void CreateEnemyInfoList()
    {
        // データを分けるための仮リストを定義
        List<EnemyInformation> addDates = default;
        List<EnemyData> enemyDates = default;

        for (int i = 0; i < enemyWaveDatas.Count; i++)
        {
            // 使用されているScriptableObjectデータ内のリストをインスタンス化
            enemyDates = enemyWaveDatas[i].enemyData;

            if (enemyDates == null || enemyDates.Count <= 0) // nullか要素数が0なら飛ばす
            {
                continue;
            }

            // 分けるデータを初期化
            addDates = new List<EnemyInformation>();

            for (int j = 0; j < enemyDates.Count; j++)
            {
                // データを一旦分けて格納
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

            // 分けたデータ内で、出現時間の降順で並べ替えして結合
            addDates.Sort((a, b) => (int)(a.appearanceTime * 100) - (int)(b.appearanceTime * 100));
            enemyInformationList.AddRange(addDates);
        }
    }

    /// <summary>
    /// 敵オブジェクトが全てFalseになっているかの確認
    /// </summary>
    /// <returns>敵が全てFalseになっているかの真偽</returns>
    private bool EnemyActiveFalseCheck()
    {
        // アクティブなオブジェクトがあるなら処理を終了
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
    /// 敵の生成を管理するメソッド
    /// </summary>
    private void InstantiateEnemyObject()
    {
        // 読み込んでいるデータが切り替わったらタイマーをリセット
        if (enemyInformationList[0].waveDateKey != nowLoadingEnemyWaveDate)
        {
            nowLoadingEnemyWaveDate = enemyInformationList[0].waveDateKey;
            elapsedTimer = 0;
        }

        // 敵生成用タイマーのカウント
        elapsedTimer += Time.deltaTime;

        // タイマーが敵生成リストの先頭の生成時間を上回っていないなら終了
        if (enemyInformationList[0].appearanceTime >= elapsedTimer)
        {
            return;
        }

        // 次生成する敵の種類を判別して生成
        if (enemyInformationList[0].isBoss)
        {
            if (EnemyActiveFalseCheck())
            {
                // 戦闘フラグをTrueに
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
    /// ザコ敵の生成メソッド
    /// </summary>
    public void InstNormalEnemy()
    {
        // 非アクティブなオブジェクトを探して、あるならば情報を渡してアクティブにする
        foreach (EnemyMove enemyObject in enemyList)
        {
            if (enemyObject.gameObject.activeSelf)
            {
                continue;
            }

            // エネミーをアクティブにして情報を渡す
            enemyObject.OnEnableInitLoad(enemyWaveDatas[enemyInformationList[0].waveDateKey], enemyInformationList[0].enemyDateKey);
            enemyObject.transform.gameObject.SetActive(true);
            return;
        }

        // 数が足りなかった場合に新規生成
        CreateNewObject();
        InstNormalEnemy();
    }

    /// <summary>
    /// ボスの生成
    /// </summary>
    private void InstBoss()
    {
        _audioSource.clip = _audioClips[1];
        _audioSource.Play();

        // ボスマネージャーにボスの情報を渡して生成してもらう
        bossManager.ActiveBoss(enemyWaveDatas[enemyInformationList[0].waveDateKey].
        enemyData[enemyInformationList[0].enemyDateKey].BossWave);
    }
}