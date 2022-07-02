using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Enemyを動かすクラス
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] int m_life = 1;
    [SerializeField] int m_maxLife = 2;
    [SerializeField] Slider m_lifeGauge = null;
    [SerializeField] float m_movePower = 10f;
    [SerializeField] float m_maxSpeed = 2f;

    [SerializeField] float m_targetRange = 4f;
    [SerializeField] float m_attackRange = 2f;

    Animator m_anim;
    GameObject m_player = null;
    Rigidbody m_rb;
    public bool m_damageFrag = false;
    bool m_dieFrag = false;

    AudioSource audioSource;
    [SerializeField] AudioClip m_damageSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_life = m_maxLife;
        m_lifeGauge.gameObject.SetActive(false);
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (m_dieFrag || !m_player) return;
        float distance = Vector3.Distance(this.transform.position, m_player.transform.position);
        if (m_damageFrag)
        {
            m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);
        }
        else if (distance < m_targetRange)
        {
            Vector3 dir = m_player.transform.position - this.transform.position;
            dir.y = 0;
            this.transform.forward = dir;

            if (distance < m_attackRange)
            {
                m_anim.SetTrigger("AttackFrag");
                m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);
            }
            else if (m_rb.velocity.magnitude < m_maxSpeed)
            {
                m_rb.AddForce(this.transform.forward * m_movePower);
            }
        }
        else
        {
            m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);
        }
    }
    public void ResetAnim()
    {
        m_damageFrag = false;
        m_anim.ResetTrigger("AttackFrag");
    }
    public void Damage()
    {
        audioSource.PlayOneShot(m_damageSound);
        m_damageFrag = true;
        m_lifeGauge.gameObject.SetActive(true);
        m_life -= 3;
        if (m_life <= 0)
        {
            m_dieFrag = true;
            m_anim.Play("Die");
            DOTween.To(() => m_lifeGauge.value,
                l =>
                {
                    m_lifeGauge.value = l;
                },
                (float)m_life / m_maxLife,
                1f);
            GameManager.Instance.SubEnemyCount();
        }
        else
        {
            m_anim.Play("GetHit");
            DOTween.To(() => m_lifeGauge.value, 
                l =>
                {
                    m_lifeGauge.value = l;
                },
                (float)m_life / m_maxLife,
                1f);
        }
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
