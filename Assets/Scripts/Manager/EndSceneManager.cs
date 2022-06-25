using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] GameObject m_text;
    float m_timer = 0;
    private void Awake()
    {
        m_text.SetActive(false);
    }
    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > 3)
        {
            m_text.SetActive(true);
            if (Input.anyKey)
            {
                SceneChanger.LoadScene("TitleScene");
            }
        }
    }
}
