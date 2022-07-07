using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオーバー時に呼びだすPlayerのPrefabにアタッチする
/// </summary>
public class PlayerGameoverController : MonoBehaviour
{
    [Tooltip("再生するアニメーション")]
    [SerializeField] Animator anim;
    
    public void AnimSpeedChange()
    {
        anim.speed = 0;
    }
}
