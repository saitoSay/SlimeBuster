using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool m_inGame = true;
    [SerializeField] GameObject m_playerPrefab;
    [SerializeField] GameObject m_enemyPrefab;
    [SerializeField] GameObject m_playerPos;
    [SerializeField] GameObject[] m_enemysPos;
    GameObject[] m_enemyArray;
    int m_enemyCount;
    [Tooltip("ゲームをクリアしてからシーン遷移するまでの時間")]
    [SerializeField] float m_offTime;
    float m_timer = 0;
    [Tooltip("目標テキストのオブジェクト")]
    [SerializeField] Text m_missionTextObj;
    [Tooltip("目標テキスト")]
    [SerializeField] string m_missionText;
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
    void Awake()
    {
        instance = this;
        GameStart();
        FadeController.StartFadeIn();
    }
    private void Update()
    {
        if (m_enemyCount <= 0)
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_offTime)
            {
                GameObject audioObj = GameObject.Find("Audio Source");
                Destroy(audioObj);
                SceneChanger.LoadScene("EndScene");
            }
        }
    }
    /// <summary>
    /// ゲーム開始処理
    /// </summary>
    public void GameStart()
    {
        m_enemyArray = new GameObject[m_enemysPos.Length];
        //敵をプレハブから出現させる
        for (int i = 0; i < m_enemysPos.Length; i++)
        {
            m_enemyArray[i] = Instantiate(m_enemyPrefab, m_enemysPos[i].transform);
        }
        m_enemyCount = m_enemyArray.Length;
        m_missionTextObj.text = m_missionText + m_enemyCount.ToString();
        m_inGame = true;
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
