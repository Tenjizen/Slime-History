using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickItem : MonoBehaviour
{
    [SerializeField] int NumberObject;
    //[SerializeField] string TagNote;
    [SerializeField] string TagIngredient;
    [SerializeField] GameObject note;
    private bool triggerPlayer;

    private void Update()
    {
        if (triggerPlayer && Input.GetKeyDown(KeyCode.E))
        {
            if (this.tag == "Note")
            {
                InventoryVisual.Instance.Notes.SetActive(true);
            }
            else if (this.tag == TagIngredient)
                InventoryVisual.Instance.Ingredients[NumberObject].transform.GetComponent<Image>().color = Color.white;


            Destroy(note);
        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            triggerPlayer = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            triggerPlayer = false;
    }

}
