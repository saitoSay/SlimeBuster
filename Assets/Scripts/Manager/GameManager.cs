using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float m_timer;
    /// <summary>残敵数</summary>
    int m_enemyCount;
    /// <summary>ゲームクリア判定</summary>
    bool m_isClear;
    bool m_inGame;
    [Tooltip("ゲームをクリアしてからシーン遷移するまでの時間")]
    [SerializeField] float m_offTime;
    [Tooltip("目標テキストのオブジェクト")]
    [SerializeField] Text m_missionTextObj;
    [Tooltip("目標テキスト")]
    [SerializeField] string m_missionText;
    [Tooltip("再生するBGMのKey")]
    [SerializeField] string m_bgmKey;
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
        if (!m_inGame)
        {
            //シーン遷移するまでの時間を計測する
            m_timer += Time.deltaTime;
            //一定時間待機してからシーン遷移を行う
            if (m_timer > m_offTime)
            {
                if (m_isClear)
                {
                    SceneChanger.LoadScene("EndScene");
                }
                else
                {
                    SceneChanger.LoadScene("GameScene");
                }
            }
        }
    }
    /// <summary>
    /// ゲーム開始処理
    /// </summary>
    public void GameStart()
    {
        EventManager.GameStart();
        SetEnemyCount();
        SoundManager.Instance.PlayBGM(m_bgmKey);
        m_inGame = true;
    }
    /// <summary>
    /// ゲームクリア処理
    /// </summary>
    public void GameClear()
    {
        EventManager.GameClear(); 
        m_inGame = false;
        //クリア判定の設定
        m_isClear = true;
    }
    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    public void GameOver()
    {
        EventManager.GameOver();
        m_inGame = false;
        //クリア判定の設定
        m_isClear = false;
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
        if(m_enemyCount <= 0)
        {
            GameClear();
        }
    }
}
