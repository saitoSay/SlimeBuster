using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float m_timer;
    int m_enemyCount;
    [SerializeField] GameObject m_playerPrefab;
    [SerializeField] GameObject m_playerPos;
    [Tooltip("ゲームをクリアしてからシーン遷移するまでの時間")]
    [SerializeField] float m_offTime;
    [Tooltip("目標テキストのオブジェクト")]
    [SerializeField] Text m_missionTextObj;
    [Tooltip("目標テキスト")]
    [SerializeField] string m_missionText;
    public static bool InGame { get; private set; }
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            //参照時に自身が存在しなければその場で生成
            if (instance == null)
            {
                var obj = new GameObject("GameManager");
                var manager = obj.AddComponent<GameManager>();
                instance = manager;
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GameStart();
    }
    private void Update()
    {
        if (m_enemyCount <= 0)
        {
            //敵の数が0以下になった時に
            m_timer += Time.deltaTime;
            //一定時間待機してから終了処理を行う
            if (m_timer > m_offTime)
            {
                GameEnd();
            }
        }
    }
    /// <summary>
    /// ゲーム開始処理
    /// </summary>
    public void GameStart()
    {
        EventManager.GameStart();
        FadeController.StartFadeIn();
        SetEnemyCount();
        InGame = true;
    }
    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    public void GameEnd()
    {
        EventManager.GameEnd();
        SceneChanger.LoadScene("EndScene");
    }
    /// <summary>
    /// 残敵数を設定する
    /// </summary>
    public void SetEnemyCount()
    {
        m_enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        m_missionTextObj.text = m_missionText + m_enemyCount.ToString();
    }
    /// <summary>
    /// 残りの敵数を減らす処理
    /// </summary>
    public void SubEnemyCount()
    {
        m_enemyCount--;
        //表示テキストを更新
        m_missionTextObj.text = m_missionText + m_enemyCount.ToString();
    }
}
