using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [Tooltip("キー入力を受け付けるまでの待ち時間")]
    [SerializeField] float m_waitKeyTime;
    [Tooltip("遷移するシーン名")]
    [SerializeField] string m_sceneName;
    [Tooltip("再生するBGMのkey")]
    [SerializeField] string m_bgmKey;
    [Tooltip("決定時のSEのkey")]
    [SerializeField] string m_selectSeKey;
    [Tooltip("表示するtext")]
    [SerializeField] Text m_text; 

    private void Start()
    {
        StartCoroutine(WaitInput(m_waitKeyTime));
        SoundManager.Instance.PlayBGM(m_bgmKey);
    }
    IEnumerator WaitInput(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_text.gameObject.SetActive(true);
        while(!Input.anyKey)
        {
            yield return null;
        }
        SoundManager.Instance.PlayOneShot(m_selectSeKey);
        FadeController.StartFadeOutIn(() => SceneChanger.LoadScene(m_sceneName));
    }
}
