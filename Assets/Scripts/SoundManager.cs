using UnityEngine;

/// <summary>音の管理をするシングルトンのクラス</summary>
public class SoundManager : MonoBehaviour
{
    AudioSource m_audioSource;
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
}
