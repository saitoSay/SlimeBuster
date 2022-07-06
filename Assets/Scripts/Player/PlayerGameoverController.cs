using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオーバー時に呼びだすPlayerのPrefabにアタッチする
/// </summary>
public class PlayerGameoverController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip sound1;
    [SerializeField] Animator anim;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
    }
    
    public void animSpeedChange()
    {
        anim.speed = 0;
    }
}
