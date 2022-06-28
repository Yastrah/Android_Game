using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    private int scene; // сцена на которую нужно перейти

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToScene(int scene) {
        this.scene = scene;
        Time.timeScale = 1f;
        anim.SetTrigger("fade");
    }

    public void OnFadeExit() {
        SceneManager.LoadScene(this.scene);
        StartCoroutine(LoadingScreenOnFade());
    }

    IEnumerator LoadingScreenOnFade() {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        while(!operation.isDone) {
            yield return null;
        }
    }
}
