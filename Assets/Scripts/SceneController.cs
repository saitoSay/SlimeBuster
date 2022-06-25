using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class SceneController : MonoBehaviour
{
    AudioSource m_audioSource;
    [SerializeField] AudioClip m_sound;
    Animator m_anim;
    bool m_flag = false;
    
    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_flag = false;
        m_audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.anyKeyDown && !m_flag)
        {
            m_anim.SetBool("Trigger", true);
            m_flag = true;
            m_audioSource.PlayOneShot(m_sound);
        }
    }
    public void LordGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
