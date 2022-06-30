using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private GameObject controllerInterface;

    private DialogueManager dialogueManager;
    private bool isOpen = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void OnWindowOpen()
    {
        isOpen = true;
        dialogueManager.DisplayNextSentence();
        controllerInterface.SetActive(false);
    }

    public void OnWindowClose()
    {
        if(isOpen) {
            isOpen = false;
            controllerInterface.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public bool getStatus() {
        return isOpen;
    }
}
