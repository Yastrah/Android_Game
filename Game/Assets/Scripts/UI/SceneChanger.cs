using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [Header("Объекты")]
    [Tooltip("Экран загрузки")]
    [SerializeField] private GameObject loadingScreen;

    private bool sceneIsActive = false;
    private int scene; // сцена на которую нужно перейти

    private Animator anim;

    public bool SceneIsActive { get { return sceneIsActive; } }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Активация начала перехода на другую сцену
    /// </summary>
    /// <param name="scene"></param>
    public void FadeToScene(int scene)
    {
        this.scene = scene;
        Time.timeScale = 1f;
        anim.SetTrigger("fade");
    }

    /// <summary>
    /// Функция, вызывающаяся при выходе со сцены
    /// </summary>
    public void OnFadeExit()
    {
        SceneManager.LoadScene(this.scene);
        StartCoroutine(LoadingScreenOnFade());
    }

    /// <summary>
    /// Функция, вызывающаяся при появлении на сцены
    /// </summary>
    public void OnFadeEnter()
    {
        sceneIsActive = true;
    }

   /// <summary>
   /// Корутина, отвечающая за отображение действий на экране загрузки
   /// </summary>
   /// <returns></returns>
    IEnumerator LoadingScreenOnFade()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
