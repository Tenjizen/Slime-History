using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripingLiquid : MonoBehaviour
{
    private PlayerController pc;
    private Rigidbody2D rb;
    private Vector2 velocity;

    [Header("Speed Value")]
    [SerializeField] private float gravityModifier = 1f;
    
    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        Vector2 deltaPos = velocity * Time.deltaTime;
        Vector2 move = Vector2.up * deltaPos.y;

        Movement(move);
    }

    private void Movement(Vector2 move)
    {
        rb.position = rb.position + move;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.Die();
        }
        Destroy(gameObject);
    }
}