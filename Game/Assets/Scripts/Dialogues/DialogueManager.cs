using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject actionButton;
    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text nameText;
    
    private Animator anim;

    private Queue<string> sentences;
    private List<char> decelerationSymbols;

    private void Start()
    {
        sentences = new Queue<string>();
        decelerationSymbols = new List<char> {'.', '!', '?', ';'};
        anim = dialogueWindow.GetComponent<Animator>();
    }

    public void StartDialogue(string dialogueId) 
    {
        Dialogue dialogue = Db.GetDialogue(dialogueId); // получение диалога из базы данных по его id

        dialogueWindow.SetActive(true);
        anim.SetBool("openDialogue", true);

        nameText.text = dialogue.title;
        dialogueText.text = "...";
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        // DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char symbol in sentence.ToCharArray())
        {
            dialogueText.text += symbol;

            if(decelerationSymbols.Contains(symbol)) {
                yield return new WaitForSeconds(0.4f);
            }
            else {
                yield return new WaitForSeconds(0.04f);
            }
        }
    }

    public void EndDialogue()
    {
        anim.SetBool("openDialogue", false);
    }
    
}
