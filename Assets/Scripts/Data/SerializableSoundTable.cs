using UnityEngine;
using System.Collections.Generic;
using System;

//ディクショナリーをインスペクターに表示させるためのクラス
//下記URL参照
//https://qiita.com/k_yanase/items/fb64ccfe1c14567a907d

public class SerializableSoundTable : MonoBehaviour
{
    [Tooltip("Key string : Value AudioClipのDictionary")]
    [SerializeField] SoundTable m_soundDictionary;
}

/// <summary>
/// ジェネリックを隠すために継承してしまう
/// </summary>
[Serializable]
public class SoundTable : Serialize.TableBase<string, AudioClip, SoundPair>
{

}

/// <summary>
/// ジェネリックを隠すために継承してしまう
/// </summary>
[Serializable]
public class SoundPair : Serialize.KeyAndValue<string, AudioClip>
{
    public SoundPair(string key, AudioClip value) : base(key, value)
    {

    }
}

