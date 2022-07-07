using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>プレイヤーアイコンの変更用コンポーネント</summary>
public class PlayerIconController : MonoBehaviour
{
    private Image m_playerIcon = null;
    [SerializeField] Sprite[] m_sprites = null;
    [SerializeField] float m_waitTime;
    const int c_defalutIconIndex = 0;
    const int c_damegeIconindex = 1;
    void Start()
    {
        m_playerIcon = GetComponent<Image>();
        //ラムダ式を利用することでイベントに登録
        PlayerController.OnDamage += () => StartCoroutine(TempChangeIcon());
        EventManager.OnGameOver += ChangeIcon;
    }
    private IEnumerator TempChangeIcon()
    {
        m_playerIcon.sprite = m_sprites[c_damegeIconindex];
        yield return new WaitForSeconds(m_waitTime);
        m_playerIcon.sprite = m_sprites[c_defalutIconIndex];
    }
    private void ChangeIcon()
    {
        //一定時間後に元のアイコンに戻るのを防ぐためコルーチンを止める
        StopCoroutine(TempChangeIcon());
        m_playerIcon.sprite = m_sprites[c_damegeIconindex];
    }
}
