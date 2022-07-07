using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Enemyを動かすクラス
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>現在の体力</summary>
    int m_currentLife = 1;
    [Tooltip("最大体力")]
    [SerializeField] int m_maxLife = 2;
    [Tooltip("加速する速さ")]
    [SerializeField] float m_movePower = 10f;
    [Tooltip("最高速度")]
    [SerializeField] float m_maxSpeed = 2f;
    [Tooltip("体力ゲージ")]
    [SerializeField] Slider m_lifeGauge = null;
    [Tooltip("プレイヤーを視認できる範囲")]
    [SerializeField] float m_targetRange = 4f;
    [Tooltip("プレイヤーに攻撃できる範囲")]
    [SerializeField] float m_attackRange = 2f;

    Animator m_enemyAnim;
    Rigidbody m_rb;
    public bool m_frozen = false;
    bool m_isAlive = false;
    const float c_hpTweenTime = 1;

    void Start()
    {
        //現在の体力を設定
        m_currentLife = m_maxLife;
        //初期状態では体力ゲージが見えないように設定
        m_lifeGauge.gameObject.SetActive(false);
        m_rb = GetComponent<Rigidbody>();
        m_enemyAnim = GetComponent<Animator>();
    }
    void Update()
    {
        if (m_isAlive || !PlayerManager.Instance.GetPlayer().IsAlive) return;

        Move();
    }
    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        Vector3 playerPos = PlayerManager.Instance.GetPlayer().transform.position;
        //プレイヤーとの距離を計算
        float distance = Vector3.Distance(this.transform.position, playerPos);

        if (distance < m_targetRange && !m_frozen)
        {
            //プレイヤーのいる方向を向く
            Vector3 dir = playerPos - this.transform.position;
            dir.y = 0;
            this.transform.forward = dir;

            if (distance < m_attackRange)
            {
                Attack();
            }
            else if (m_rb.velocity.magnitude < m_maxSpeed)
            {
                //最高速度未満の場合は加速する
                m_rb.AddForce(this.transform.forward * m_movePower);
            }
        }
        else
        {
            m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);
        }
    }
    /// <summary>
    /// 攻撃処理
    /// </summary>
    private void Attack()
    {
        m_enemyAnim.SetTrigger("AttackFrag");
    }
    /// <summary>
    /// アニメーション終了処理
    /// </summary>
    public void ResetAnim()
    {
        m_frozen = false;
        m_enemyAnim.ResetTrigger("AttackFrag");
    }
    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    public void Damage(int damage)
    {
        m_frozen = true;
        m_lifeGauge.gameObject.SetActive(true);
        SoundManager.Instance.PlayOneShot("Hit");

        m_currentLife -= damage;
        if (m_currentLife <= 0)
        {
            m_isAlive = true;
            m_enemyAnim.Play("Die");
            GameManager.Instance.SubEnemyCount();
        }
        else
        {
            m_enemyAnim.Play("GetHit");
        }
        //DOTweenを使い、HPゲージを滑らかに減らす
        DOTween.To(() => m_lifeGauge.value,
            value =>
            {
                m_lifeGauge.value = value;
            },
            //現在の体力と最大体力の割合まで減らす
            (float)m_currentLife / m_maxLife,
            //処理にかける時間を設定
            c_hpTweenTime);
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
