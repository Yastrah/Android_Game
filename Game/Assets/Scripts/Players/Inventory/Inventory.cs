using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // public bool[] isFull;
    // public GameObject[] slots;
    public GameObject inventory;
    public GameObject controllerInterface;
    
    [HideInInspector] public bool isOpen = false;

    public void OpenInventory() {
        if(!isOpen) {
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
