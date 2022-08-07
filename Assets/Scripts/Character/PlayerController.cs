using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// プレイヤーを操作するクラス
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerController : MonoBehaviour, IEvent
{
    [Tooltip("プレイヤーのデータ")]
    [SerializeField] PlayerData m_playerData;

    [Tooltip("攻撃時の当たり判定")]
    [SerializeField] GameObject m_attackCollider = null;
    [Tooltip("ゲームオーバー時に切り替えるプレハブ")]
    [SerializeField] GameObject m_gameoverPrefab;

    /// <summary>現在の体力</summary>
    int m_life = 1;
    /// <summary>攻撃力を取得するためのプロパティ</summary>
    public int AttackPower { get => m_playerData.GetAttackPower(); }
    /// <summary>プレイヤーが生きているか確認するプロパティ</summary>
    public bool IsAlive { get; private set; }
    /// <summary>体力ゲージ</summary>
    Slider m_lifeGauge = null;

    Rigidbody m_rb;
    Animator m_anim;
    EnemyDetector m_enemyDetector = null;

    /// <summary>攻撃中か確認する</summary>
    bool m_isAttacking;
    /// <summary>hpゲージを減らす時間</summary>
    const float c_hpTweenTime = 1f;

    public static event Action OnDamage;
    public static void Damage() => OnDamage?.Invoke();

    void Start()
    {
        //各コンポーネントの取得
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_enemyDetector = GetComponent<EnemyDetector>();
        SetLifeGauge();
        SetEvent();
        //カメラのFollowをPlayerに設定する
        PlayerManager.Instance.SetCamera();
        IsAlive = true;
        m_isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack"))
        {
            Damage();
        }
    }
    public void Move(float v, float h)
    {
        //入力した方向をベクトルに変換する
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            //入力が無い場合はxz平面の速度を0にする
            m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
        }
        else
        {
            //カメラの向きに合わせた方向にベクトルを変換する
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            //速度を設定する
            Vector3 velo = dir.normalized * m_playerData.GetMovingSpeed();
            m_rb.velocity = velo;

            //攻撃中は回転しないようリターン
            if (m_isAttacking) return;
            Quaternion targetRotation = Quaternion.LookRotation(dir);

            //Slerpを使い回転処理を滑らかにする
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation ,
                Time.deltaTime * m_playerData.GetTurnSpeed());

        }
        //現在の速度に応じたアニメーションを行うために速度の大きさをアニメーターに渡す
        m_anim.SetFloat("Speed", m_rb.velocity.magnitude);
    }
    /// <summary>
    /// 攻撃処理
    /// </summary>
    public void Attack()
    {
        if (m_isAttacking) return;
        if (m_enemyDetector.m_lockon)
        {
            //ロックオンしている敵の方向を向く
            this.transform.LookAt(m_enemyDetector.Target.transform.position);
        }
        m_isAttacking = true;
        m_anim.SetTrigger("fire");
    }
    /// <summary>
    /// 攻撃判定を出す
    /// </summary>
    public void SetAttackCollider()
    {
        m_attackCollider.SetActive(true);
    }
    /// <summary>
    /// 攻撃判定を消す
    /// </summary>
    public void ResetAttackCollider()
    {
        m_attackCollider.SetActive(false);
    }
    public void AttackEnd()
    {
        m_isAttacking = false;
    }

    /// <summary>
    /// 体力ゲージを設定する
    /// </summary>
    private void SetLifeGauge()
    {
        m_lifeGauge = GameObject.FindGameObjectWithTag("LifeGauge").GetComponent<Slider>();
    }
    private void SubHp()
    {
        m_life--;
        m_anim.Play("Damaged");
        //DOTweenを使い、HPゲージを滑らかに減らす
        DOTween.To(() => m_lifeGauge.value,
            value =>
            {
                if (m_life <= 0)
                {
                    //複数回Dead関数が呼ばれる事を防ぐ
                    if (IsAlive)
                    {
                        Dead();
                    }
                }
                m_lifeGauge.value = value;
            },
            //現在の体力と最大体力の割合まで減らす
            (float)m_life / m_playerData.GetMaxLife(),
            //処理にかける時間を設定
            c_hpTweenTime);

    }
    /// <summary>
    /// 死亡処理
    /// </summary>
    private void Dead()
    {
        IsAlive = false;
        SoundManager.Instance.PlayOneShot("Dead");
        //オブジェクトを非表示にし、ゲームオーバー用のプレハブと入れ替える
        gameObject.SetActive(false);
        Instantiate(m_gameoverPrefab, this.gameObject.transform.position, this.transform.rotation);
    }
    /// <summary>
    /// 攻撃時にサウンドを再生する
    /// </summary>
    public void AttackSoundPlay()
    {
        SoundManager.Instance.PlayOneShot("Attack");
    }
    /// <summary>
    /// 被弾時にサウンドを再生する
    /// </summary>
    private void DamageSoundPlay()
    {
        SoundManager.Instance.PlayOneShot("Damage");
    }
    public void SetEvent()
    {
        OnDamage += SubHp;
        OnDamage += () => m_isAttacking = false;

        EventManager.OnGameClear += RemoveEvent;
        EventManager.OnGameOver += RemoveEvent;
    }
    public void RemoveEvent()
    {
        OnDamage -= SubHp;
        EventManager.OnGameClear -= RemoveEvent;
        EventManager.OnGameOver -= RemoveEvent;
    }
}
