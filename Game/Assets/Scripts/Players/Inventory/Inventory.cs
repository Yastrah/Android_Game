using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("������� ���������")]
    [Tooltip("������ ������ ���������")]
    [SerializeField] private GameObject inventory;

    [Header("�������������� �������")]
    [Tooltip("Canvas � ���������� ����������")]
    [SerializeField] private GameObject controllerInterface;

    [Tooltip("������ ����������� ����")]
    [SerializeField] private DialogueWindow dialogueWindow;
    
    [HideInInspector] public bool isOpen = false;

    /// <summary>
    /// ������� �������� ���������
    /// </summary>
    public void OpenInventory() {
        // �������� �� ��, ������ �� ��� ���������, � �� ���� �� ������ ������
        if(!isOpen && !dialogueWindow.getStatus()) {
            isOpen = true;
            inventory.SetActive(true);

            controllerInterface.SetActive(false);
        }
    }

    /// <summary>
    /// ������� �������� ���������
    /// </summary>
    public void CloseInventory() {
        if(isOpen) {
            isOpen = false;
            inventory.SetActive(false);

            controllerInterface.SetActive(true);
        }
    }
}
