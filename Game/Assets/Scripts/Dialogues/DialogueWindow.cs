using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
{
    [Header("Дополнительные объекты")]
    [Tooltip("Canvas с элементами управления")]
    [SerializeField] private GameObject controllerInterface;

    private DialogueManager dialogueManager;
    private bool isOpen = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    /// <summary>
    /// Функция, вызывающаяся по окончании анимации появления
    /// </summary>
    public void OnWindowOpen()
    {
        isOpen = true;
        dialogueManager.DisplayNextSentence();
        controllerInterface.SetActive(false);
    }

    /// <summary>
    /// Функция, вызывающаяся по окончании анимации скрытия
    /// </summary>
    public void OnWindowClose()
    {
        if(isOpen) {
            isOpen = false;
            controllerInterface.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Получение текущего статуса окна диалогов
    /// </summary>
    /// <returns>bool Открыто ли окно диалогов</returns>
    public bool getStatus() {
        return isOpen;
    }
}
