﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [Tooltip("フェードスピード")]
    [SerializeField] private float m_fadeSpeed = 1f;
    [Tooltip("フェードする画像")]
    [SerializeField] private Image m_fadeImage = default;
    [Tooltip("開始時の色")]
    [SerializeField] private Color m_startColor = Color.black;

    private static FadeController instance = default;
    public static FadeController Instance
    {
        get
        {
            //参照時に自身が無ければ黒いFade用Imageを作成し、自身をインスタンス化する
            if (instance == null)
            {
                var obj = new GameObject("FadeCanvas");
                obj.AddComponent<RectTransform>();
                var canvas = obj.AddComponent<Canvas>();
                canvas.sortingOrder = 20;
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                var fadeController = obj.AddComponent<FadeController>();
                var imageObj = new GameObject("FadeImage");
                imageObj.transform.SetParent(obj.transform);
                var imageRect = imageObj.AddComponent<RectTransform>();
                var image = imageObj.AddComponent<Image>();
                imageRect.sizeDelta = new Vector2(2000, 1100);
                imageRect.localPosition = Vector2.zero;
                image.color = Color.black;
                fadeController.m_fadeImage = image;
                instance = fadeController;
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ChangeFadeSpeed(float speed)
    {
        m_fadeSpeed = speed;
    }
    /// <summary>
    /// フェードイン後にアクションする
    /// </summary>
    /// <param name="action"></param>
    public static void StartFadeIn(Action action = null)
    {
        Instance.StartCoroutine(Instance.FadeIn(action));
    }
    /// <summary>
    /// フェードアウト後にアクションする
    /// </summary>
    /// <param name="action"></param>
    public static void StartFadeOut(Action action = null)
    {
        Instance.StartCoroutine(Instance.FadeOut(action));
    }
    public static void StartFadeOutIn(Action outAction = null, Action inAction = null)
    {
        Instance.StartCoroutine(Instance.FadeOutIn(outAction, inAction));
    }
    IEnumerator FadeIn(Action action)
    {
        m_fadeImage.gameObject.SetActive(true);
        yield return FadeIn();
        action?.Invoke();
        m_fadeImage.gameObject.SetActive(false);
    }
    IEnumerator FadeOut(Action action)
    {
        m_fadeImage.gameObject.SetActive(true);
        yield return FadeOut();
        action?.Invoke();
    }
    IEnumerator FadeOutIn(Action outAction, Action inAction)
    {
        m_fadeImage.gameObject.SetActive(true);
        yield return FadeOut();
        outAction?.Invoke();
        yield return FadeIn();
        inAction?.Invoke();
        m_fadeImage.gameObject.SetActive(false);
    }
    IEnumerator FadeIn()
    {
        float clearScale = 1f;
        Color currentColor = m_startColor;
        while (clearScale > 0f)
        {
            clearScale -= m_fadeSpeed * Time.deltaTime;
            if (clearScale <= 0f)
            {
                clearScale = 0f;
            }
            currentColor.a = clearScale;
            m_fadeImage.color = currentColor;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float clearScale = 0f;
        Color currentColor = m_startColor;
        while (clearScale < 1f)
        {
            clearScale += m_fadeSpeed * Time.deltaTime;
            if (clearScale >= 1f)
            {
                clearScale = 1f;
            }
            currentColor.a = clearScale;
            m_fadeImage.color = currentColor;
            yield return null;
        }
    }
}