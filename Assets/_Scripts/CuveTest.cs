using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuveTest : MonoBehaviour
{
    [Header("Liste de l'ordre des boutons")]
    [SerializeField] int[] pushButtonListSolution = new int[3];

    int[] pushButtonList = new int[] {-1, -1, -1};

    int number = 0;
    int isGood = 0;

    public void pushButton(int buttonNumber)
    {
        if (buttonNumber == pushButtonListSolution[number])
        {
            pushButtonList[number] = buttonNumber;
            number++;
        }
        else
        {
            number = 0;
            pushButtonList = new int[] { -1, -1, -1};
        }

        for (int i = 0; i < pushButtonListSolution.Length; i++)
        {
            if (pushButtonList[i] == pushButtonListSolution[i])
            {
                isGood++;
            }
        }

        if (isGood == 3)
        {
            //Cuves OK
        }
        else if (isGood < 3)
        {
            isGood = 0;
        }
    }
}
