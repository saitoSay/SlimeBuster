using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoundAssets")]
public class SoundAssets : ScriptableObject
{
    [Tooltip("Key string : Value AudioClipのDictionary")]
    [SerializeField] SoundTable m_soundDictionary;
    public AudioClip GetAudioClip(string key)
    {
        return m_soundDictionary.GetTable()[key];
    }
}
