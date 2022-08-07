using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Playerのデータクラス</summary>
[CreateAssetMenu(menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Tooltip("移動速度")]
    [SerializeField] float m_movingSpeed = 7f;
    [Tooltip("回転速度")]
    [SerializeField] float m_turnSpeed = 5f;
    [Tooltip("体力の最大値")]
    [SerializeField] int m_maxLife = 3;
    [Tooltip("攻撃力")]
    [SerializeField] int m_attackPower = 3;

    public float GetMovingSpeed() { return m_movingSpeed; }
    public float GetTurnSpeed() { return m_turnSpeed; }
    public int GetMaxLife() { return m_maxLife; }
    public int GetAttackPower() { return m_attackPower; }
}
