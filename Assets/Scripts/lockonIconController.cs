using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>アイコンを回転させるクラス</summary>
public class lockonIconController : MonoBehaviour
{
    [Tooltip("回転速度")]
    [SerializeField] float m_rotateSpeed;
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.Rotate(0, 0, m_rotateSpeed);
    }
}
