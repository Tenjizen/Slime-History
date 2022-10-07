using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryVisual : MonoBehaviour
{
    [SerializeField] GameObject Inventory;
    public List<GameObject> Notes;
    public List<GameObject> Ingredients;
    private PauseMenu pauseMenu;

    public static InventoryVisual Instance;
    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();
        foreach (var item in Ingredients)
        {
            item.SetActive(false);
        }
        foreach (var note in Notes)
        {
            note.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pauseMenu.SetGamePause();
            pauseMenu.PauseGame();
            SetInventory();
        }
    }
    private void SetInventory()
    {
        Inventory.SetActive(!Inventory.activeSelf);
    }
}
