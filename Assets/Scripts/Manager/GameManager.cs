using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    [SerializeField] GameObject textObj;
    Text text;
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
        if (gameStartFrag)
        {
            ReStart();
        }
        m_enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        m_enemyCount = m_enemyArray.Length;
    }
    private void Start()
    {
        text = textObj.GetComponent<Text>();
    }
    private void Update()
    {
        text.text = "残りスライムの数 : " + m_enemyCount.ToString();
        if (m_enemyCount <= 0)
        {
            m_timer += Time.deltaTime;
            if (m_timer > 3)
            {
                GameObject audioObj = GameObject.Find("Audio Source");
                Destroy(audioObj);
                SceneManager.LoadScene(2);
            }
        }
    }
    public void ReStart()
    {
        //Instantiate(m_playerPrefab, m_playerPos.transform);
        for (int i = 0; i < m_enemysPos.Length; i++)
        {
            Instantiate(m_enemyPrefab, m_enemysPos[i].transform);
        }
        gameStartFrag = false;
        GameOverFadeOut.Instance.StartScene();
    }
}
