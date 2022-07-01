using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverFadeOut : MonoBehaviour
{
    Animator m_anim;
    bool m_flag = false;
    GameObject m_gameoverPlayer;
    public static GameOverFadeOut Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        m_flag = false;
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        m_gameoverPlayer = GameObject.FindGameObjectWithTag("Finish");
        if (m_gameoverPlayer && !m_flag)
        {
            m_anim.SetBool("SceneBool", true);
            m_flag = true;
        }
    }
    public void StartScene()
    {
        m_anim.Play("FadeinAnim");
        m_flag = false;
    }
    public void LordGameScene()
    {
        SceneChanger.LoadScene("GameScene");
    }
    public void ChangeFrag()
    {
        GameManager.m_inGame = true;
    }
}
