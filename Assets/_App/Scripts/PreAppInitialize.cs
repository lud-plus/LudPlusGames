#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 開発用、UnityEditor上で動作
/// 一番最初に呼ばれる
/// Play時に、シーンを保存する
/// </summary>
[InitializeOnLoad]
public static class PreAppInitialize
{
    static PreAppInitialize()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        }
    }
}
#endif

