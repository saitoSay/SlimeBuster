using System;

/// <summary>
/// イベントを管理するクラス
/// </summary>
public class EventManager
{
    /// <summary>ゲーム開始処理を登録する</summary>
    public static event Action OnGameStart;
    /// <summary>ゲーム終了処理を登録する</summary>
    public static event Action OnGameEnd;

    /// <summary>ゲーム開始時に呼ぶ</summary>
    public static void GameStart() => OnGameStart?.Invoke();
    /// <summary>ゲーム終了時に呼ぶ</summary>
    public static void GameEnd() => OnGameEnd?.Invoke();
}