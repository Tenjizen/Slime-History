using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    [Header("Dialog Box & script")]
    [SerializeField] GameObject textPanel;
    [SerializeField] GameObject dialogCinematique;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        textPanel.SetActive(true);
        dialogCinematique.SetActive(true);
    }
}
