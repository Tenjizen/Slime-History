using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePanelAppear : MonoBehaviour
{
    [Header("Code UI gameObject")]
    [SerializeField] GameObject codePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        codePanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        codePanel.SetActive(false);
    }
}
