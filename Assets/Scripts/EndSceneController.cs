using System.Collections;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] float m_waitTime;

    private void Start()
    {
        StartCoroutine(WaitInput(m_waitTime));
    }
    IEnumerator WaitInput(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(true);
        if (Input.anyKey)
        {
            SceneChanger.LoadScene("TitleScene");
        }
    }
}
