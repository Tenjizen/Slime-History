using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogsManager : MonoBehaviour
{
    Queue<string> sentences;

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogTxt;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        nameTxt.text = dialog.name;

        sentences.Clear();

        foreach (string sentence in sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogTxt.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogTxt.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {

    }
}
