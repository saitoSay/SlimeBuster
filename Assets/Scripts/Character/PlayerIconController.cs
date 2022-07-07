using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>プレイヤーアイコンの変更用コンポーネント</summary>
public class PlayerIconController : MonoBehaviour, IEvent
{
    private Image m_playerIcon = null;
    [SerializeField] Sprite[] m_sprites = null;
    [SerializeField] float m_waitTime;
    const int c_defalutIconIndex = 0;
    const int c_damegeIconindex = 1;
    Action m_action; 
    void Start()
    {
        m_playerIcon = GetComponent<Image>();
        //のちにイベントの登録を解除するためにメンバー変数内に格納しておく
        m_action = () => StartCoroutine(ChangeIcon());
        SetEvent();
    }
    /// <summary>
    /// アイコンを変更する
    /// </summary>
    private IEnumerator ChangeIcon()
    {
        m_playerIcon.sprite = m_sprites[c_damegeIconindex];
        if (PlayerManager.Instance.GetPlayer().IsAlive)
        {
            //死亡時はアイコンを変えたまま関数を抜ける
            yield break;
        }
        yield return new WaitForSeconds(m_waitTime);
        m_playerIcon.sprite = m_sprites[c_defalutIconIndex];
    }

    public void SetEvent()
    {
        PlayerController.OnDamage += m_action;
        EventManager.OnGameClear += RemoveEvent;
    }

    public void RemoveEvent()
    {
        PlayerController.OnDamage -= m_action;
        EventManager.OnGameClear -= RemoveEvent;
    }
}
