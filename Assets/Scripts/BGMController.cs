using UnityEngine;

/// <summary>BGMの管理をするシングルトンのクラス</summary>
public class BGMController : MonoBehaviour
{
    private static BGMController instance;
    public static BGMController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("BGMController");
                var controller = obj.AddComponent<BGMController>();
                instance = controller;
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
}
