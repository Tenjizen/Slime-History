using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuvesIsOk : MonoBehaviour
{
    [Header("Cuves")]
    [SerializeField] GameObject RedCuve;
    [SerializeField] GameObject BlueCuve;
    [SerializeField] GameObject YellowCuve;

    [Header("Boutons")]
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    
    public void CuveDone()
    {
        if (RedCuve.transform.localScale.y == 0.5f && BlueCuve.transform.localScale.y == 0.5f && YellowCuve.transform.localScale.y == 0.5f)
        {
            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);
        }
    }
}
