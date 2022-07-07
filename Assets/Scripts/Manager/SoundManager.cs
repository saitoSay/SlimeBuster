using UnityEngine;

/// <summary>音の管理をするシングルトンのクラス</summary>
public class SoundManager : MonoBehaviour
{
    AudioSource m_audioSource;
    /// <summary>音源のアセット</summary>
    SoundAssets m_soundAssets;
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            //参照時にインスタンスが無ければ自身を生成する
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
        //音源データを読み込む
        m_soundAssets = Resources.Load<SoundAssets>("SoundAssets");

        //デフォルトの音量が大きすぎたため調整
        m_audioSource.volume = m_soundAssets.GetVolume();
    }
    /// <summary>
    /// BGMをAudioSourceに設定する
    /// </summary>
    private void SetBGM(string bgmKey)
    {
        m_audioSource.clip = m_soundAssets.GetAudioClip(bgmKey);
    }
    /// <summary>
    /// BGMを再生する
    /// </summary>
    public void PlayBGM(string bgmKey)
    {
        SetBGM(bgmKey);
        m_audioSource.Play();
    }
    /// <summary>
     /// SEを再生する
     /// </summary>
    public void PlayOneShot(string soundKey)
    {
        m_audioSource.PlayOneShot(m_soundAssets.GetAudioClip(soundKey));
    }
}
