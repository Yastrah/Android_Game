using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private PauseManager pauseManager;
    private SceneChanger sc;

    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        sc = FindObjectOfType<SceneChanger>();
    }

    private void Save()
    {
        // Write
    }

    private void Load()
    {
        // Write
    }

    /// <summary>
    /// Функция, срабатывающая при сворачивании приложения
    /// </summary>
    /// <param name="hasFocus"></param>
    private void OnApplicationFocus(bool hasFocus)
    {
        // Если приложение свернули 
        if (!hasFocus && sc.SceneIsActive)
        {
            pauseManager.Pause();
        }
    }

    private void OnApplicationQuit() {
        // Write
    }
}
