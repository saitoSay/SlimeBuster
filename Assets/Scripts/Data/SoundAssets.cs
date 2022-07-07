using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>音源のデータをディクショナリーとして保存するクラス</summary>
[CreateAssetMenu(menuName = "SoundAssets")]
public class SoundAssets : ScriptableObject
{
    [Tooltip("Key string : Value AudioClipのDictionary")]
    [SerializeField] SoundTable m_soundDictionary;
    /// <summary>
    /// 音源を取得する
    /// </summary>
    /// <param name="key">音源に対応したキー</param>
    /// <returns>音源</returns>
    public AudioClip GetAudioClip(string key)
    {
        return m_soundDictionary.GetTable()[key];
    }
}
