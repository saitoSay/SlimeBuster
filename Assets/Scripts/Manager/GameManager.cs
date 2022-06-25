using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameStartFrag = true;
    [SerializeField] GameObject m_playerPrefab;
    [SerializeField] GameObject m_enemyPrefab;
    [SerializeField] GameObject m_playerPos;
    [SerializeField] GameObject[] m_enemysPos;
    GameObject[] m_enemyArray;
    public int m_enemyCount;
    float m_timer = 0;
    [SerializeField] Text m_textObj;
    [SerializeField] string m_missionText;
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("GameManager");
                var manager = obj.AddComponent<GameManager>();
                instance = manager;
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
        if (gameStartFrag)
        {
            ReStart();
        }
        m_enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        m_enemyCount = m_enemyArray.Length;
        m_textObj.text = m_missionText + m_enemyCount.ToString();
    }
    private void Update()
    {
        m_textObj.text = "残りスライムの数 : " + m_enemyCount.ToString();
        if (m_enemyCount <= 0)
        {
            m_timer += Time.deltaTime;
            if (m_timer > 3)
            {
                GameObject audioObj = GameObject.Find("Audio Source");
                Destroy(audioObj);
                SceneChanger.LoadScene("EndScene");
            }
        }
    }
    public void ReStart()
    {
        for (int i = 0; i < m_enemysPos.Length; i++)
        {
            Instantiate(m_enemyPrefab, m_enemysPos[i].transform);
        }
        gameStartFrag = false;
        GameOverFadeOut.Instance.StartScene();
    }
    public void SubEnemyCount()
    {
        m_enemyCount--;
        m_textObj.text = m_missionText + m_enemyCount.ToString();
    }
}
