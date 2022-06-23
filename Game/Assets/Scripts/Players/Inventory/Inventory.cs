using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // public bool[] isFull;
    // public GameObject[] slots;
    public GameObject inventory;
    public GameObject joystick;
    public GameObject shootButton;
    
    public bool isOpen = false;

    void Start()
    {
        // isOpen = false;
    }

    public void OpenInventory() {
        if(!isOpen) {
            isOpen = true;
            inventory.SetActive(true);

            joystick.SetActive(false);
            shootButton.SetActive(false);
        }
    }

    public void CloseInventory() {
        if(isOpen) {
            isOpen = false;
            inventory.SetActive(false);

            joystick.SetActive(true);
            shootButton.SetActive(true);
        }
    }
}
