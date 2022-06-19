using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int scene; // сцена на которую нужно перейти
    [SerializeField] private GameObject loadingScreen;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToScene() {
        Time.timeScale = 1f;
        anim.SetTrigger("fade");
    }

    public void OnFadeExit() {
        SceneManager.LoadScene(scene);
        StartCoroutine(LoadingScreenOnFade());
    }

    IEnumerator LoadingScreenOnFade() {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        // Debug.Log("IEnumerator");
        while(!operation.isDone) {
            // Debug.Log("not done");
            yield return null;
        }
    }
}
