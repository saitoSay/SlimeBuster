using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverFadeOut : MonoBehaviour
{
    Animator m_anim;
    bool m_frag = false;
    GameObject m_gameoverPlayer;
    public static GameOverFadeOut Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        m_frag = false;
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        m_gameoverPlayer = GameObject.FindGameObjectWithTag("Finish");
        if (m_gameoverPlayer && !m_frag)
        {
            m_anim.SetBool("SceneBool", true);
            m_frag = true;
        }
    }
    public void StartScene()
    {
        m_anim.Play("FadeinAnim");
        m_frag = false;
    }
    public void LordGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeFrag()
    {
        GameManager.gameStartFrag = true;
    }
}
