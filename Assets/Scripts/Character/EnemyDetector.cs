using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Tooltip("索敵範囲")]
    [SerializeField] float m_targetRange = 4f;
    [Tooltip("敵の検出を行う間隔（単位: 秒）")]
    [SerializeField] float m_detectInterval = 1f;
    float m_timer;
    public bool m_lockon = false;

    /// <summary>ロックオン時に表示するアイコン</summary>
    GameObject[] images;
    /// <summary>
    /// ロックオンしている敵
    /// </summary>
    public GameObject Target { get; private set; }
    private void Start()
    {
        images = GameObject.FindGameObjectsWithTag("Image");
        foreach (var item in images)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        //ロックオン切り替え
        if (Input.GetButtonDown("Fire2"))
        {
            Lock();
        }
        if (!Target)
        {
            m_lockon = false;
        }


        m_timer += Time.deltaTime;

        // 一定間隔で検出を行う
        if (m_timer > m_detectInterval)
        {
            m_timer = 0;
            Search();
        }

        // ロックオンしているターゲットが索敵範囲外に出たらロックオンをやめる
        if (Target && m_targetRange < Vector3.Distance(this.transform.position, Target.transform.position))
        {
            TargetOut();
        }
    }

    /// <summary>ターゲットを外す</summary>
    private void TargetOut()
    {
        Target = null;
        //ロックオンアイコンを非表示にする
        foreach (var item in images)
        {
            item.SetActive(false);
        }
    }

    private void Search()
    {
        // シーン内の敵を取得する
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

        //距離を計り、一番近い敵をターゲットに設定する
        foreach (var enemy in enemyArray)
        {
            float distance = Vector3.Distance(this.transform.position, enemy.transform.position);

            if (distance < m_targetRange)
            {
                if (Target == null || distance < Vector3.Distance(this.transform.position, Target.transform.position) && !m_lockon)
                {
                    Target = enemy;
                }
            }
        }
    }
    /// <summary>ロックオンの切り替えをする</summary>
    private void Lock()
    {
        //ロックオン出来るターゲットがいた時
        if (Target && !m_lockon)
        {
            //ロックオン状態にする
            m_lockon = true;
            GameObject canvas = Target.transform.Find("Canvas").gameObject;
            GameObject lockonIcon = canvas.transform.Find("Image").gameObject;
            //アイコンを表示
            lockonIcon.SetActive(true);
        }
        else
        {
            //ロックオン状態の解除
            m_lockon = false;
            foreach (var item in images)
            {
                item.SetActive(false);
            }
        }
    }
}
