using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>音源のデータをディクショナリーとして保存するクラス</summary>
[CreateAssetMenu(menuName = "SoundAssets")]
public class SoundAssets : ScriptableObject
{
    [Tooltip("Key string : Value AudioClipのDictionary")]
    [SerializeField] SoundTable m_soundDictionary;
    [Tooltip("音量")]
    [SerializeField] float m_volume;

    /// <summary>
    /// 音源を取得する
    /// </summary>
    /// <param name="key">音源に対応したキー</param>
    /// <returns>音源</returns>
    public AudioClip GetAudioClip(string key)
    {
        return m_soundDictionary.GetTable()[key];
    }
    /// <summary>
    /// 音量を返す
    /// </summary>
    /// <returns>音量</returns>
    public float GetVolume()
    {
        return m_volume;
    }
    /// <summary>
    /// 音量を設定する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetVolume(float volume)
    {
        //例外処理
        if (volume > 1)volume = 1;
        if (volume < 0)volume = 0;
        m_volume = volume;
    }
}
