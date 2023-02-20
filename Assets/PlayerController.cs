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

    public SwordAttack swordAttack;
    SpriteRenderer spriteRenderer;

    Animator animator;

    bool canMove = true;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private bool TryMove(Vector2 direction)
    {

        if (direction == Vector2.zero) return false;

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
        if (!canMove) return;
        //if movementInput is not zero, then move the player
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if (!success && movementInput.x > 0)
            {
                //try moving in the x direction
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success && movementInput.y > 0)
                {
                    //try moving in the y direction
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", success);

        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Set direction of sprite to movement direction
        spriteRenderer.flipX = movementInput.x < 0;


    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

    }

    void OnFire(InputValue value)
    {
        animator.SetTrigger("swordAttack");
    }
    public void LockMovement()
    {
        canMove = false;
    }

    public void SwordAttack()
    {
        LockMovement();
        if (spriteRenderer.flipX)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }


    }

    public void EndSwordAttack()
    {
        swordAttack.StopAttack();
        unLockMovement();
    }

    public void unLockMovement()
    {
        canMove = true;
    }

}
