using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Db
{
    private static Dictionary<string, string[]> dialoguesSentences = new Dictionary<string, string[]>() {
        ["d1"] = new string[] {"Я квадрат, меня здесь поставили кривые разработчики. А все потому, что им было лень делать НПС для теста диалогов. Ну разве это справедливо? Мне кажется что нет.", "Я нахожусь на этой сцене. Но у меня нет возможности что-то продавать. Разрабы совсем жестокие!", "Если ты все же хочешь что-нибудь купить, то иди к продавцу..."},
        ["d2"] = new string[] {"я торговец", "возможно я что-то продаю", "не уходи, и загляни в мой магазин", "в нем продается много всего"},
    };

    private static Dictionary<string, string> dialoguesTitle = new Dictionary<string, string>() {
        ["d1"] = "Square",
        ["d2"] = "NPC"
    };

    public static Dialogue GetDialogue(string dialogueId) {
        Dialogue dialogue = new Dialogue(dialoguesTitle[dialogueId], dialoguesSentences[dialogueId]);

        return dialogue;
    }

}
