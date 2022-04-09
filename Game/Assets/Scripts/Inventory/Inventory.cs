using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // public bool[] isFull;
    // public GameObject[] slots;
    public GameObject inventory;
    
    private bool isOpen;

    void Start()
    {
        isOpen = false;
    }

    public void OpenInventory() {
        if(!isOpen) {
            isOpen = true;
            inventory.SetActive(true);
        }
    }

    public void CloseInventory() {
        if(isOpen) {
            isOpen = false;
            inventory.SetActive(false);
        }
    }
}
