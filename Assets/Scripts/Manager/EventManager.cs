using System;

/// <summary>
/// イベントを管理するクラス
/// </summary>
public class EventManager
{
    /// <summary>ゲーム開始処理を登録する</summary>
    public static event Action OnGameStart;
    /// <summary>ゲームクリア処理を登録する</summary>
    public static event Action OnGameClear;
    /// <summary>ゲームオーバー処理を登録する</summary>
    public static event Action OnGameOver
        ;

    /// <summary>ゲーム開始時に呼ぶ</summary>
    public static void GameStart() => OnGameStart?.Invoke();
    /// <summary>ゲームクリア時に呼ぶ</summary>
    public static void GameClear() => OnGameClear?.Invoke();
    /// <summary>ゲームオーバー時に呼ぶ</summary>
    public static void GameOver() => OnGameOver?.Invoke();
}
/// <summary>
/// イベントを登録するクラスに実装するインターフェース
/// </summary>
interface IEvent
{
    /// <summary>
    /// イベントを登録する
    /// </summary>
    void SetEvent();
    /// <summary>
    /// イベントを削除する
    /// </summary>
    void RemoveEvent();

}