using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI codeTxt;
    [SerializeField] string codeTextValue = "";

    [Header("Mettre le code à trouver ici")]
    [SerializeField] string code;

    [Header("Porte à ouvrir")]
    [SerializeField] GameObject door;

    [Header("Trigger du Panel")]
    [SerializeField] GameObject triggerActive;

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
        codeTxt.text = codeTextValue;

        if (codeTextValue == code)
        {
            door.SetActive(false);
            triggerActive.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }  

        else if (codeTextValue.Length >= 4)
        {
            codeTextValue = "";
            codeTxt.text = codeTextValue;
        }
    }
}
