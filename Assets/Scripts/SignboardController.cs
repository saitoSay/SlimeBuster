using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>看板を操作するクラス</summary>
public class SignboardController : MonoBehaviour
{
    [SerializeField] Animator m_anim;
    private void ShowBoard()
    {
        m_anim.Play("boradShow");
    }
    private void CloseBorad()
    {
        m_anim.Play("boradClose");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowBoard();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CloseBorad();
        }
    }
}
