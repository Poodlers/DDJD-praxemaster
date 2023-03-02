using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    public Collider2D swordCollider;
    SpriteRenderer spriteRenderer;

    PlayerController playerController;

    Animator animator;
    Vector2 rightAttackOffset;

    Vector2 leftAttackOffset;

    private void Start()
    {

        rightAttackOffset = transform.position;
        leftAttackOffset = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }


    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;

    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = leftAttackOffset;


    }
    public void AttackUp()
    {
        swordCollider.enabled = true;
        swordCollider.transform.Rotate(0, 0, 90);
        swordCollider.transform.localPosition = new Vector2(0.01f, 0.066f);
        spriteRenderer.transform.localPosition = new Vector2(0.01f, 0.066f);


    }

    public void AttackDown()
    {
        swordCollider.enabled = true;
        swordCollider.transform.Rotate(0, 0, 90);
        swordCollider.transform.localPosition = new Vector2(0,
        -0.16f);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
        swordCollider.transform.rotation = Quaternion.identity;
        if (spriteRenderer.flipX)
            swordCollider.transform.localPosition = rightAttackOffset;
        else
            swordCollider.transform.localPosition = leftAttackOffset;
    }

    public void StartAttack()
    {
        playerController.LockMovement();
        if (animator.GetBool("isMovingUp"))
        {
            AttackUp();
        }
        else if (animator.GetBool("isMovingDown"))
        {
            AttackDown();
        }
        else if (spriteRenderer.flipX)
        {
            AttackLeft();
        }
        else
        {
            AttackRight();
        }


    }

    public void EndSwordAttack()
    {
        StopAttack();
        playerController.unLockMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Sword hit " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController =
             collision.gameObject.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.TakeDamage(1);
            }
        }
    }
}
