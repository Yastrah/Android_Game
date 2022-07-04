using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Объекты инвентаря")]
    [Tooltip("Объект панели инвентаря")]
    [SerializeField] private GameObject inventory;

    [Header("Дополнительные объекты")]
    [Tooltip("Canvas с элементами управления")]
    [SerializeField] private GameObject controllerInterface;

    [Tooltip("Скрипт диалогового окна")]
    [SerializeField] private DialogueWindow dialogueWindow;
    
    [HideInInspector] public bool isOpen = false;

    /// <summary>
    /// Фуекция открытия инвентаря
    /// </summary>
    public void OpenInventory() {
        // Проверка на то, открыт ли уже инвентарь, и не идет ли сейчас диалог
        if(!isOpen && !dialogueWindow.getStatus()) {
            isOpen = true;
            inventory.SetActive(true);

            controllerInterface.SetActive(false);
        }
    }

    /// <summary>
    /// Функция закрытия инвентаря
    /// </summary>
    public void CloseInventory() {
        if(isOpen) {
            isOpen = false;
            inventory.SetActive(false);

            controllerInterface.SetActive(true);
        }
    }
}
