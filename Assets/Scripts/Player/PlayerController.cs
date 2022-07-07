using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// プレイヤー
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("移動速度")]
    [SerializeField] float m_movingSpeed = 5f;
    [Tooltip("回転速度")]
    [SerializeField] float m_turnSpeed = 3f;
    [Tooltip("攻撃時の当たり判定")]
    [SerializeField] GameObject m_attackCollider = null;
    [Tooltip("現在の体力")]
    [SerializeField] int m_life = 1;
    [Tooltip("体力の最大値")]
    [SerializeField] int m_maxLife = 2;
    [Tooltip("攻撃力")]
    [SerializeField] int m_attackPower = 3;
    public int AttackPower { get => m_attackPower; }
    [SerializeField] Slider m_lifeGauge = null;
    Rigidbody m_rb;
    Animator m_anim;
    EnemyDetector m_enemyDetector = null;
    bool m_isAlive;

    /// <summary>プレイヤーの情報</summary>
    [Tooltip("ゲームオーバー時に切り替えるプレハブ")]
    [SerializeField] GameObject m_gameoverPrefab;

    public static event Action OnDamage;
    public static void Damage() => OnDamage?.Invoke();
    public static PlayerController Instance { get; private set; }
    void Start()
    {
        Instance = this;
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_enemyDetector = GetComponent<EnemyDetector>();
        OnDamage += SubHp;
        m_isAlive = true;
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        
        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_enemyDetector.m_lockonFrag)
            {
                this.transform.LookAt(m_enemyDetector.Target.transform.position);
            }
            m_anim.SetTrigger("fire");
        }
        else
        {
            m_anim.ResetTrigger("fire");
        }
        
        if (dir == Vector3.zero)
        {
            m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
        }
        else
        {
            dir = Camera.main.transform.TransformDirection(dir); 
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);  // Slerp を使うのがポイント

            Vector3 velo = dir.normalized * m_movingSpeed; 
            m_rb.velocity = velo;
        }
    }
    void LateUpdate()
    {
        Vector3 horizontalVelocity = m_rb.velocity;
        horizontalVelocity.y = 0;
        m_anim.SetFloat("Speed", horizontalVelocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack")
        {
            Damage();
        }
    }

    public void BiginAttack()
    {
        m_attackCollider.SetActive(true);
    }
    public void EndAttack()
    {
        m_attackCollider.SetActive(false);
    }

    private void SubHp()
    {
        m_life--;
        m_anim.Play("Damaged");
        //DOTweenを使い、HPゲージを滑らかに減らす
        DOTween.To(() => m_lifeGauge.value,
            l =>
            {
                if (m_life <= 0)
                {
                    if (m_isAlive)
                    {
                        Dead();
                    }
                }
                m_lifeGauge.value = l;
            },
            (float)m_life / m_maxLife,
            1f);

    }
    private void Dead()
    {
        m_isAlive = false;
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
        Instantiate(m_gameoverPrefab, this.gameObject.transform.position, this.transform.rotation);
    }
    public void AttackSoundPlay()
    {
        SoundManager.Instance.PlayOneShot("Attack");
    }
    public void DamageSoundPlay()
    {
        SoundManager.Instance.PlayOneShot("Damage");
    }
}
