using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンを変更させるクラス
/// </summary>
public class SceneChanger : MonoBehaviour
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
