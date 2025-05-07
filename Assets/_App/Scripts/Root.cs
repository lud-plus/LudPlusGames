using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// アプリケーションのRootシーン
/// 起動時に呼ばれ、指定のシーンに分岐する
/// </summary>
public class Root : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("MainGame_001");
    }

    [RuntimeInitializeOnLoadMethod]
    private static void AppInitialize()
    {
        Debug.Log("=== AppInitialize ===");

        SceneManager.LoadScene("Root");
    }
}

