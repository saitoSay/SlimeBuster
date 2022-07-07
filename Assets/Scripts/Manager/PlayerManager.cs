using UnityEngine;
using Cinemachine;

/// <summary>プレイヤーを管理するクラス</summary>
public class PlayerManager : MonoBehaviour
{
    /// <summary>プレイヤー</summary>
    PlayerController m_player;
    [Tooltip("メインカメラ")]
    [SerializeField] CinemachineVirtualCamera m_camera;
    public static PlayerManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (!m_player.IsAlive)
        {
            GameManager.Instance.GameOver();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            m_player.Attack();
        }

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        m_player.Move(v, h);

    }
    /// <summary>
    /// カメラをプレイヤーに追従させるため登録をする
    /// </summary>
    public void SetCamera()
    {
        if (m_player == null)
        {
            SetPlayer();
        }
        m_camera.Follow = m_player.transform;
    }
    /// <summary>
    /// プレイヤーをセットする
    /// </summary>
    private void SetPlayer()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    /// <summary>
    /// プレイヤーの情報を返す
    /// </summary>
    public PlayerController GetPlayer()
    {
        return m_player;
    }
}
