using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>プレイヤーアイコンの変更用コンポーネント</summary>
public class PlayerIconController : MonoBehaviour, IEvent
{
    Image m_playerIcon = null;
    bool m_damageFlag;
    float m_timer;
    [Tooltip("アイコンを設定する")]
    [SerializeField] Sprite[] m_sprites = null;
    [Tooltip("アイコンが変わっている時間")]
    [SerializeField] float m_waitTime;
    const int c_defalutIconIndex = 0;
    const int c_damegeIconindex = 1; 
     private void Start()
    {
        m_damageFlag = false;
        m_timer = 0;
        m_playerIcon = GetComponent<Image>();
        SetEvent();
    }
    private void Update()
    {
        ChangeIcon();
    }
    /// <summary>
    /// アイコンを変更する
    /// </summary>
    private void ChangeIcon()
    {
        if (!m_damageFlag) return;
        m_playerIcon.sprite = m_sprites[c_damegeIconindex];

        m_timer += Time.deltaTime;
        if (m_timer < m_waitTime || !PlayerManager.Instance.GetPlayer().IsAlive) return;

        m_playerIcon.sprite = m_sprites[c_defalutIconIndex];
    }
    private void ChangeDamageFlag()
    {
        m_damageFlag = true;
    }
    public void SetEvent()
    {
        PlayerController.OnDamage += ChangeDamageFlag;
        EventManager.OnGameClear += RemoveEvent;
    }

    public void RemoveEvent()
    {
        PlayerController.OnDamage -= ChangeDamageFlag;
        EventManager.OnGameClear -= RemoveEvent;
    }
}
