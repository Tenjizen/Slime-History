using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsTrigger : MonoBehaviour
{
    public Dialog dialog;

    public void TriggerDialog()
    {
        FindObjectOfType<DialogsManager>().StartDialog(dialog);
    }
}
