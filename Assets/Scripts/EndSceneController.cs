using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    [SerializeField]float m_timer = 0;
    

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > 3)
        {
            this.gameObject.SetActive(true);
            if (Input.anyKey)
            {
                SceneChanger.LoadScene("TitleScene");
            }
        }
    }
}
