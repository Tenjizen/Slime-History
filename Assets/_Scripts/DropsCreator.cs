using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsCreator : MonoBehaviour
{
    [Header("Prefab de la gougoute")]
    [SerializeField] GameObject drop;

    [Header("Temps entre chaque gouttes")]
    [SerializeField] float createSpeed;

    void Start()
    {
        StartCoroutine(DropCreator());
    }
    
    IEnumerator DropCreator()
    {
        Instantiate(drop);


        yield return new WaitForSeconds(createSpeed);
        StartCoroutine(DropCreator());
    }
}
