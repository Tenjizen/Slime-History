using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuveButton : MonoBehaviour
{
    [Header("Cuves connectées au boutons")]
    [Header("Cuve -0.5f")]
    [SerializeField] GameObject cuveDown;
    [Header("Cuve +0.5f")]
    [SerializeField] GameObject cuveUp;

    [Header("Verification des cuves")]
    [SerializeField] CuvesIsOk okCuves;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cuveDown.transform.localScale.y <= 0f)
        {
            cuveDown.transform.localScale = new Vector3(1f, 0f, 1f);
        }
        else
        {
            cuveDown.transform.localScale += new Vector3(0f, -0.5f, 0f);
        }

        if (cuveUp.transform.localScale.y >= 1f)
        {
            cuveUp.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            cuveUp.transform.localScale += new Vector3(0f, 0.5f, 0f);
        }

        okCuves.CuveDone();
    }
}
