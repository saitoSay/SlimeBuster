using UnityEngine;

/// <summary>音の管理をするシングルトンのクラス</summary>
public class SoundManager : MonoBehaviour
{
    AudioSource m_audioSource;
    SoundAssets m_soundAssets;
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("SoundManager");
                var controller = obj.AddComponent<SoundManager>();
                instance = controller;
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        m_soundAssets = Resources.Load<SoundAssets>("SoundAssets");
    }
    private void SetBGM(string bgmKey)
    {
        m_audioSource.clip = m_soundAssets.GetAudioClip(bgmKey);
    }
    public void PlayBGM(string bgmKey)
    {
        SetBGM(bgmKey);
        m_audioSource.Play();
    }
    public void PlayOneShot(string soundKey)
    {
        m_audioSource.PlayOneShot(m_soundAssets.GetAudioClip(soundKey));
    }
}
