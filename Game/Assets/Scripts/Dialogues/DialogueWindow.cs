using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
{
    [Header("�������������� �������")]
    [Tooltip("Canvas � ���������� ����������")]
    [SerializeField] private GameObject controllerInterface;

    private DialogueManager dialogueManager;
    private bool isOpen = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    /// <summary>
    /// �������, ������������ �� ��������� �������� ���������
    /// </summary>
    public void OnWindowOpen()
    {
        isOpen = true;
        dialogueManager.DisplayNextSentence();
        controllerInterface.SetActive(false);
    }

    /// <summary>
    /// �������, ������������ �� ��������� �������� �������
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
    /// ��������� �������� ������� ���� ��������
    /// </summary>
    /// <returns>bool ������� �� ���� ��������</returns>
    public bool getStatus() {
        return isOpen;
    }
}
