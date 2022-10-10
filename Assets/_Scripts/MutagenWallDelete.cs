using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutagenWallDelete : MonoBehaviour
{
    [Header("Wall")]
    [SerializeField] GameObject wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wall.SetActive(false);
        }
    }
}
