using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    /// <summary>索敵範囲</summary>
    [SerializeField] float m_targetRange = 4f;
    /// <summary>敵の検出を行う間隔（単位: 秒）</summary>
    [SerializeField] float m_detectInterval = 1f;
    public bool m_lockon = false;
    float m_timer;

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
            if (Target && !m_lockon)
            {
                m_lockon = true;
                GameObject canvas = Target.transform.Find("Canvas").gameObject;
                GameObject lockonIcon = canvas.transform.Find("Image").gameObject;
                images = GameObject.FindGameObjectsWithTag("Image");
                foreach (var item in images)
                {
                    item.SetActive(false);
                }
                lockonIcon.SetActive(true);
            }
            else
            {
                m_lockon = false;
                images = GameObject.FindGameObjectsWithTag("Image");
                foreach (var item in images)
                {
                    item.SetActive(false);
                }
            }
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

            // シーン内の敵を全て取得する
            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

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

        // ロックオンしているターゲットが索敵範囲外に出たらロックオンをやめる
        if (Target)
        {
            if (m_targetRange < Vector3.Distance(this.transform.position, Target.transform.position))
            {
                Target = null;
                images = GameObject.FindGameObjectsWithTag("Image");
                foreach (var item in images)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}
