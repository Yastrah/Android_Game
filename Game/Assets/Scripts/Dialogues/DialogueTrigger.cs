using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("�������������� �������")]
    [Tooltip("������ ��������������")]
    [SerializeField] private GameObject actionButton;
    

    [Header("��������� �������")]
    [Tooltip("Id �������")]
    [SerializeField] private string dialogueId;

    private Interaction interaction;
    private DialogueManager dialogueManager;

    private void Start()
    {
        interaction = actionButton.GetComponent<Interaction>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            interaction.type = Interaction.InteractionType.Dialogue;
            interaction.ActionName = dialogueId;

            actionButton.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            interaction.type = Interaction.InteractionType.None;
            interaction.ActionName = "";

            actionButton.SetActive(false);
        }
    }
}
