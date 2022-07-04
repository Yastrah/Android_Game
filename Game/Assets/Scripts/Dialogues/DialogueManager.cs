using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Объекты диалога")]
    [Tooltip("Окно диалога")]
    [SerializeField] private GameObject dialogueWindow;

    [Tooltip("Основной текст")]
    [SerializeField] private Text dialogueText;

    [Tooltip("Заголовок")]
    [SerializeField] private Text nameText;
    
    private Animator anim;

    private string sentence = null;
    private Queue<string> sentences;
    private List<char> decelerationSymbols;

    private void Start()
    {
        sentences = new Queue<string>();
        decelerationSymbols = new List<char> {'.', '!', '?', ';'};
        anim = dialogueWindow.GetComponent<Animator>();
    }

    /// <summary>
    /// Начало диалога. Отображение панели диалога
    /// </summary>
    /// <param name="dialogueId"></param>
    public void StartDialogue(string dialogueId) 
    {
        Dialogue dialogue = Db.GetDialogue(dialogueId); // получение диалога из базы данных по его id

        dialogueWindow.SetActive(true);
        anim.SetBool("openDialogue", true);

        nameText.text = dialogue.title;
        dialogueText.text = "...";
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
    }

    /// <summary>
    /// Отображение следующего текста, или моментальное отображение текущего текста
    /// </summary>
    public void DisplayNextSentence()
    {
        // если нужно моментально вывести оставшийся текст
        if(dialogueText.text != sentence && sentence != null) {
            StopAllCoroutines();
            dialogueText.text = sentence;
            return;
        }
        
        // если тексты кончились
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }

        sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Корутина, овечающая за медленную печать текста
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char symbol in sentence.ToCharArray())
        {
            dialogueText.text += symbol;

            if (decelerationSymbols.Contains(symbol))
            {
                yield return new WaitForSeconds(0.4f);
            }
            else
            {
                yield return new WaitForSeconds(0.04f);
            }
        }
    }

    /// <summary>
    /// Завершение диалога и анимация скрытия панели диалогов
    /// </summary>
    public void EndDialogue()
    {
        anim.SetBool("openDialogue", false);
        sentence = null;
    }
    
}
