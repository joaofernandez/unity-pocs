using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;
    public Transform movePoint;
    public LayerMask stopMovement;

    private readonly int SPEED = 5;

    private void Awake()
    {
        movePoint.parent = null;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        // move camera along character
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

        // Free movement
        // rb.MovePosition(rb.position + movement * SPEED * Time.fixedDeltaTime);

        // Grid movement
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(movement.x) == 1f && Mathf.Abs(movement.y) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, movement.y, 0f), 0.2f, stopMovement))
                    movePoint.position += new Vector3(movement.x, movement.y, 0f);
            }
            else if (Mathf.Abs(movement.x) == 0f && Mathf.Abs(movement.y) == 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 0f, 0f), 0.2f, stopMovement))
                    movePoint.position += new Vector3(0f, 0f, 0f);
            }
            else if (Mathf.Abs(movement.x) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f, 0f), 0.2f, stopMovement))
                    movePoint.position += new Vector3(movement.x, 0f, 0f);
            }
            else if (Mathf.Abs(movement.y) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), 0.2f, stopMovement))
                    movePoint.position += new Vector3(0f, movement.y, 0f);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, SPEED * Time.fixedDeltaTime);
        }

    }
}
