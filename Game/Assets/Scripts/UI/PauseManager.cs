using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Tooltip("Объект, содержащий панель паузы")]
    [SerializeField] private GameObject panel;

    /// <summary>
    /// Пауза
    /// </summary>
    public void Pause() {
            panel.SetActive(true);
            Time.timeScale = 0f;
    }

    /// <summary>
    /// Снять с паузы
    /// </summary>
    public void Resume() {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

}
