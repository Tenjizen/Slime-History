using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Positions départ / arrivée")]
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;

    [Header("Vitesse")]
    [SerializeField] float speed;

    Vector3 nextPos;

    private void Start()
    {
        nextPos = pos1.position;
    }

    private void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
