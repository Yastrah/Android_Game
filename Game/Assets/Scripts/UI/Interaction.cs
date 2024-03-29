using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [HideInInspector] public InteractionType type;
    [HideInInspector] public string ActionName = ""; // имя weapon, или id диалога, или имя окна магазина
    [HideInInspector] public DialogueData dialogue;

    /// <summary>
    /// Все типы взаимодействия
    /// </summary>
    public enum InteractionType {
        None,
        Dialogue,
        PickUpWeapon,
        OpenWindow,
        GameAction
    }
    
    /// <summary>
    /// Функция, вызывающаяся при нажатии на кнопку "Action"
    /// </summary>
    public void TriggerAction() {
        switch(type) {
            case InteractionType.Dialogue:
                FindObjectOfType<DialogueManager>().StartDialogue(ActionName);
                break;
            
            case InteractionType.PickUpWeapon:
                
                break;

            case InteractionType.OpenWindow:
                
                break;

            case InteractionType.GameAction:
                
                break;
        }
    }
}
