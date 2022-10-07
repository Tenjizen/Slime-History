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

    private void Update()
    {

        if (codeTextValue == code)
        {
            //DoorOpen
        }
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
        codeTxt.text = codeTextValue;

        if (codeTextValue == code)
        {
            door.SetActive(false);
        }  

        else if (codeTextValue.Length >= 4)
        {
            codeTextValue = "";
            codeTxt.text = codeTextValue;
        }
    }
}
