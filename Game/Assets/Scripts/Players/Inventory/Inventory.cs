using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // public bool[] isFull;
    // public GameObject[] slots;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject controllerInterface;
    [SerializeField] private DialogueWindow dialogueWindow;
    
    [HideInInspector] public bool isOpen = false;

    public void OpenInventory() {
        if(!isOpen && !dialogueWindow.getStatus()) {
            isOpen = true;
            inventory.SetActive(true);

            controllerInterface.SetActive(false);
        }
    }

    public void CloseInventory() {
        if(isOpen) {
            isOpen = false;
            inventory.SetActive(false);

            controllerInterface.SetActive(true);
        }
    }
}
