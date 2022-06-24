using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
