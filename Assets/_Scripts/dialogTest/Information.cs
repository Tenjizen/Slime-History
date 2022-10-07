using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Information
{
    [TextArea]
    [SerializeField] List<string> _dialog;

    public List<string> Dialog
    {
        get { return _dialog; }
    }


}
