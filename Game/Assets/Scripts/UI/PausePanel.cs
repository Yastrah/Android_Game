using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
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

    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus) {
            Pause();
        }
    }

    // public void Menu() {
    //     SceneManager.LoadScene("MainMenu");
    //     Time.timeScale = 1f;
    // }



    // private void OnApplicationPause(bool pauseStatus) {}
    // private void OnApplicationQuit() {}
    // private void OnApplicationFocus(bool focusStatus) {}
}
