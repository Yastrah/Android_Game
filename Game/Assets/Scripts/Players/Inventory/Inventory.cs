using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // public bool[] isFull;
    // public GameObject[] slots;
    public GameObject inventory;
    public GameObject leftJoystick;
    public GameObject rightJoystick;
    
    public bool isOpen = false;

    void Start()
    {
        // isOpen = false;
    }

    public void OpenInventory() {
        if(!isOpen) {
            isOpen = true;
            inventory.SetActive(true);

            leftJoystick.SetActive(false);
            rightJoystick.SetActive(false);
        }
    }

    public void CloseInventory() {
        if(isOpen) {
            isOpen = false;
            inventory.SetActive(false);

            leftJoystick.SetActive(true);
            rightJoystick.SetActive(true);
        }
    }
}
