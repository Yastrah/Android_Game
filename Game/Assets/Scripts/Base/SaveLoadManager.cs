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
    /// �������, ������������� ��� ������������ ����������
    /// </summary>
    /// <param name="hasFocus"></param>
    private void OnApplicationFocus(bool hasFocus)
    {
        // ���� ���������� �������� 
        if (!hasFocus && sc.SceneIsActive)
        {
            pauseManager.Pause();
        }
    }

    private void OnApplicationQuit() {
        // Write
    }
}
