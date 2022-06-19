using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel; // объект, содержащий ранель паузы

    public void Pause() {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    // public void Menu() {
    //     SceneManager.LoadScene("MainMenu");
    //     Time.timeScale = 1f;
    // }



    // private void OnApplicationPause(bool pauseStatus) {}
    // private void OnApplicationQuit() {}
    // private void OnApplicationFocus(bool focusStatus) {}
}
