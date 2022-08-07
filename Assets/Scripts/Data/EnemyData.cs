using UnityEngine;
using UnityEngine.UI;

/// <summary>Enemyのデータクラス</summary>
[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Tooltip("最大体力")]
    [SerializeField] int m_maxLife = 2;
    [Tooltip("加速する速さ")]
    [SerializeField] float m_movePower = 10f;
    [Tooltip("最高速度")]
    [SerializeField] float m_maxSpeed = 2f;
    [Tooltip("プレイヤーを視認できる範囲")]
    [SerializeField] float m_targetRange = 4f;
    [Tooltip("プレイヤーに攻撃できる範囲")]
    [SerializeField] float m_attackRange = 2f;
    public int GetMaxLife() { return m_maxLife; }
    public float GetMovePower() { return m_movePower; }
    public float GetMaxSpeed() { return m_maxSpeed; }
    public float GetTargetRange() { return m_targetRange; }
    public float GetAttackRange() { return m_attackRange; }
}
