using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue
{
    public string title;
    public string[] sentences;

    public Dialogue() {}

    public Dialogue(string title, string[] sentences) {
        this.title = title;
        this.sentences = sentences;
    }
}
