using System;
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

    Animator swordAnimator;

    public const float invincibilityDurationSeconds = 1.5f;
    float invincibilityDeltaTime = 0.15f;

    // todo - tidy up
    public bool isRanged = true;
    //

    bool isInvincible = false;
    public bool isDefeated = false;
    public Sprite deadSprite;
    public SwordAttack swordAttack;
    SpriteRenderer spriteRenderer;

    SpriteRenderer colherSprite;
    private GameObject model;
    Animator animator;
    public HealthBar healthBar;
    bool canMove = true;
    public int health = 5;
    public int Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
            else
            {
                Damaged();

                StartCoroutine(BecomeTemporarilyInvincible());
            }
        }
        get
        {
            return health;
        }
    }

    void Start()
    {
        healthBar.SetMaxHealth(health);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        model = GetComponentInChildren<SpriteRenderer>().gameObject;
        swordAnimator = swordAttack.GetComponent<Animator>();
        colherSprite = swordAttack.GetComponent<SpriteRenderer>();
    }
    private void Defeated()
    {
        animator.SetTrigger("Defeated");
        animator.enabled = false;
        spriteRenderer.sprite = deadSprite;
        canMove = false;
        isDefeated = true;
    }
    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
    }
    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (model.transform.localScale == Vector3.one)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one);
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        Debug.Log("Player is no longer invincible!");
        ScaleModelTo(Vector3.one);
        isInvincible = false;
    }


    // Start is called before the first frame update


    public void Damaged()
    {

        Debug.Log("Player Damaged");
    }
    public void PlayerDeath()
    {
        spriteRenderer.sprite = deadSprite;
        canMove = false;
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

            if (movementInput.x != 0)
            {
                animator.SetBool("isMovingSideways", success);
                swordAnimator.SetBool("isMovingSideways", success);
                animator.SetBool("isMovingUp", false);
                swordAnimator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", false);
                swordAnimator.SetBool("isMovingDown", false);
            }
            else if (movementInput.y < 0)
            {
                animator.SetBool("isMovingDown", success);
                swordAnimator.SetBool("isMovingDown", success);
                animator.SetBool("isMovingUp", false);
                swordAnimator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingSideways", false);
                swordAnimator.SetBool("isMovingSideways", false);
            }
            else
            {
                animator.SetBool("isMovingUp", success);
                swordAnimator.SetBool("isMovingUp", success);
                animator.SetBool("isMovingDown", false);
                swordAnimator.SetBool("isMovingDown", false);
                animator.SetBool("isMovingSideways", false);
                swordAnimator.SetBool("isMovingSideways", false);
            }


        }
        else
        {
            animator.SetBool("isMovingSideways", false);
            swordAnimator.SetBool("isMovingSideways", false);
            animator.SetBool("isMovingUp", false);
            swordAnimator.SetBool("isMovingUp", false);
            animator.SetBool("isMovingDown", false);
            swordAnimator.SetBool("isMovingDown", false);
        }

        // Set direction of sprite to movement direction
        spriteRenderer.flipX = movementInput.x < 0;
        colherSprite.flipX = spriteRenderer.flipX;
        colherSprite.transform.position = new Vector3(transform.position.x * (spriteRenderer.flipX ? -1 : 1), transform.position.y, transform.position.z);

    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (isRanged) {
            // Instantiate();
        } else { 
            swordAnimator.SetTrigger("swordAttack");
        }
    }
    public void LockMovement()
    {
        canMove = false;
    }



    public void unLockMovement()
    {
        canMove = true;
    }

    public void TakeDamage(int damage)
    {

        Health -= damage;
        healthBar.SetHealth(health);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player collided with " + collision.gameObject.name);
        if (isInvincible) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


    }


}
