using UnityEngine;


public class SwordAttack : MonoBehaviour
{

    public BoxCollider2D swordCollider;
    SpriteRenderer spriteRenderer;
    PlayerController playerController;
    const int frameRate = 50;
    int framesM2_Count = 0;
    public int M2_cooldown = 3;

    bool canDoM2 = true;
    Animator swordAnimator;
    Vector2 rightAttackOffset;

    Vector2 regularBoxColliderSize;

    Vector2 leftAttackOffset;

    public M2_cooldown m2_cooldown;

    private void Awake()
    {

        rightAttackOffset = transform.position;
        m2_cooldown.SetMaxCooldown(M2_cooldown * frameRate);
        regularBoxColliderSize = swordCollider.size;
        leftAttackOffset = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();

    }



    void FixedUpdate()
    {

        swordAnimator.SetBool("isMovingSideways", playerController.animator.GetBool("isMovingSideways"));
        swordAnimator.SetBool("isMovingUp", playerController.animator.GetBool("isMovingUp"));
        swordAnimator.SetBool("isMovingDown", playerController.animator.GetBool("isMovingDown"));
        spriteRenderer.flipX = playerController.spriteRenderer.flipX;
        transform.localPosition = spriteRenderer.flipX ? new Vector3(-0.075f, 0, 0) : new Vector3(0.075f, 0, 0);
        if (!canDoM2)
        {
            framesM2_Count++;
            m2_cooldown.SetCoolDown(framesM2_Count);
            if (framesM2_Count >= frameRate * M2_cooldown)
            {
                canDoM2 = true;
                framesM2_Count = 0;
            }
        }
    }

    public void OnFire()
    {
        swordAnimator.SetTrigger("swordAttack");
    }

    public void OnRightClick()
    {
        if (canDoM2)
        {
            canDoM2 = false;
            m2_cooldown.SetCoolDown(framesM2_Count);
            swordAnimator.SetBool("spinAttack", true);
        }

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

    public void SpinAttack()
    {
        swordCollider.enabled = true;
        swordCollider.size = new Vector2(swordCollider.size.x * 2, swordCollider.size.y * 2);
        swordCollider.transform.localPosition = new Vector2(-0.01f, 0);
        spriteRenderer.transform.localPosition = new Vector2(-0.01f, 0);
        spriteRenderer.transform.localScale = new Vector2(1.5f, 1.5f);

    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
        swordCollider.transform.rotation = Quaternion.identity;
        swordCollider.size = regularBoxColliderSize;
        spriteRenderer.transform.localScale = new Vector2(1, 1);
        if (spriteRenderer.flipX)
            swordCollider.transform.localPosition = rightAttackOffset;
        else
            swordCollider.transform.localPosition = leftAttackOffset;
    }

    public void StartAttack()
    {
        playerController.LockMovement();
        if (swordAnimator.GetBool("spinAttack"))
        {
            SpinAttack();
            swordAnimator.SetBool("spinAttack", false);
        }
        else if (swordAnimator.GetBool("isMovingUp"))
        {
            AttackUp();
        }
        else if (swordAnimator.GetBool("isMovingDown"))
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
