using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogCinematique : MonoBehaviour
{
    //for GD
    [SerializeField] Information _information;
    [SerializeField] Information _information2;
    [SerializeField] Information _information3;
    [SerializeField] Information _information4;
    [SerializeField] Information _information5;
    [SerializeField] Information _information6;


    //for GP
    [SerializeField] TextMeshProUGUI text;
    private int _currentLine = 0;
    private int _currentCine = 0;
    private void Update()
    {
        if (_currentCine == 0)
        {
            text.text = _information.Dialog[_currentLine];
        }
        else if (_currentCine == 1)
        {
            text.text = _information2.Dialog[_currentLine];
        }
        else if (_currentCine == 2)
        {
            text.text = _information3.Dialog[_currentLine];
        }
        else if (_currentCine == 3)
        {
            text.text = _information4.Dialog[_currentLine];
        }
        else if (_currentCine == 4)
        {
            text.text = _information5.Dialog[_currentLine];
        }
        else if (_currentCine == 5)
        {
            text.text = _information6.Dialog[_currentLine];
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentCine == 0)
            {
                if (_currentLine == _information.Dialog.Count - 1)
                {
                    _currentCine++;
                    _currentLine=0;
                    //fin de ciné faut desactiver le gameobject canvas du text (a mettre a chaque else if evidemment vu comment c'est bien dégueulasse c: )
                    //apres faut juste réactiver le gameobjcet a chaque cinématique avec des triggers (check y a tout sur didi)
                }
                else
                    _currentLine++;
            }
            else if (_currentCine == 1)
            {
                if (_currentLine == _information2.Dialog.Count - 1)
                {
                    _currentCine++;
                    _currentLine = 0;
                }
                else
                    _currentLine++;
            }
            else if (_currentCine == 2)
            {
                if (_currentLine == _information3.Dialog.Count - 1)
                {
                    _currentCine++;
                    _currentLine = 0;
                }
                else
                    _currentLine++;
            }
            else if (_currentCine == 3)
            {
                if (_currentLine == _information4.Dialog.Count - 1)
                {
                    _currentCine++;
                    _currentLine = 0;
                }
                else
                    _currentLine++;
            }
            else if (_currentCine == 4)
            {
                if (_currentLine == _information5.Dialog.Count - 1)
                {
                    _currentCine++;
                    _currentLine = 0;
                }
                else
                    _currentLine++;
            }
            else if (_currentCine == 5)
            {
                if (_currentLine == _information6.Dialog.Count - 1)
                {
                    _currentCine++;
                    _currentLine = 0;
                }
                else
                    _currentLine++;
            }
        }

    }
}

