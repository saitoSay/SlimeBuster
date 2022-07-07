using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>UIを回転させるクラス</summary>
public class UIRotate : MonoBehaviour
{
    [Tooltip("回転速度")]
    [SerializeField] float m_rotateSpeed;
    RectTransform m_rectTransform;
    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        m_rectTransform.Rotate(0, 0, m_rotateSpeed);
    }
}
