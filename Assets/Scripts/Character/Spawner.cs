using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトをスポーンさせるクラス
/// スポーンさせたいポジションのオブジェクトにアタッチする
/// </summary>
public class Spawner : MonoBehaviour
{
    [Tooltip("スポーンさせるプレハブ")]
    [SerializeField] GameObject m_prefab;
    private void Awake()
    {
        Spawn();
    }
    /// <summary>
    /// プレハブを自身の座標に生成する
    /// </summary>
    public void Spawn()
    {
        Instantiate(m_prefab, this.gameObject.transform);
    }
}
