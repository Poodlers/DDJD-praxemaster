using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Vector2 movementInput;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
           direction,
           movementFilter,
           castCollisions,
           moveSpeed * Time.fixedDeltaTime + collisionOffset
           );

        if (count == 0)
        {
            rb.MovePosition(rb.position +
            direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    void FixedUpdate()
    {
        //if movementInput is not zero, then move the player
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if (!success)
            {
                //try moving in the x direction
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success)
                {
                    //try moving in the y direction
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

        }

    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

    }
}
