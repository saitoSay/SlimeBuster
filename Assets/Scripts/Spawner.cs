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
        EventManager.OnGameStart += Spawn;
        EventManager.OnGameClear += () => EventManager.OnGameStart -= Spawn;
    }
    public void Spawn()
    {
        Instantiate(m_prefab, this.gameObject.transform);
    }
}
