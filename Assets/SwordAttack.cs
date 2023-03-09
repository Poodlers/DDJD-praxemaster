using UnityEngine;


public class SwordAttack : MonoBehaviour
{

    GameObject colherGFX;
    GameObject colherCollider;

    SpriteRenderer colherRenderer;
    BoxCollider2D swordCollider;
    PlayerController playerController;
    const int frameRate = 50;
    int framesM2_Count = 0;
    public int M2_cooldown = 3;
    public float spinTime = 0.5f;
    bool canDoM2 = true;
    Animator swordAnimator;
    Vector2 rightAttackOffset;

    Vector2 regularBoxColliderSize;

    Vector2 leftAttackOffset;
    Vector3 regularScale;

    M2_cooldown m2_cooldown;

    public UpgradeMenu upgradeMenu;

    private void Awake()
    {

        colherGFX = GameObject.Find("ColherGFX");
        colherRenderer = colherGFX.GetComponent<SpriteRenderer>();
        colherCollider = GameObject.Find("ColherCollider");
        swordCollider = colherCollider.GetComponent<BoxCollider2D>();
        m2_cooldown = GameObject.Find("M2_Cooldown").GetComponent<M2_cooldown>();
        regularScale = transform.localScale;
        rightAttackOffset = transform.position;
        m2_cooldown.SetMaxCooldown(M2_cooldown * frameRate);
        regularBoxColliderSize = swordCollider.size;
        leftAttackOffset = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
        swordAnimator = colherGFX.GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    public void UpgradeColherRange()
    {

        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        regularScale = transform.localScale;
        Debug.Log("UpgradeColherRange");
        upgradeMenu.ResumeGame();

    }
    void FixedUpdate()
    {

        swordAnimator.SetBool("isMovingSideways", playerController.animator.GetBool("isMovingSideways"));
        swordAnimator.SetBool("isMovingUp", playerController.animator.GetBool("isMovingUp"));
        swordAnimator.SetBool("isMovingDown", playerController.animator.GetBool("isMovingDown"));

        if (!swordCollider.enabled)
        {
            colherRenderer.flipX = playerController.spriteRenderer.flipX;
            transform.localPosition = colherRenderer.flipX ? new Vector3(-0.05f, 0, 0) : new Vector3(0.05f, 0, 0);
        }
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
        colherCollider.transform.localPosition = new Vector3(-0.3f, 0, 0);
        colherGFX.transform.localPosition = leftAttackOffset;

    }
    public void AttackUp()
    {
        swordCollider.enabled = true;
        colherCollider.transform.Rotate(0, 0, 90);
        transform.localPosition = new Vector3(0f, 0.09f, 0);

    }

    public void AttackDown()
    {
        swordCollider.enabled = true;

        transform.localPosition = new Vector3(0, -0.16f, 0);
    }
    public void SpinAttack()
    {
        swordCollider.enabled = true;
        swordCollider.size = new Vector2(swordCollider.size.x * 2.5f, swordCollider.size.y * 2.5f);
        transform.localPosition = new Vector3(-0.1f, 0, 0);
        transform.localScale = new Vector3(1.5f * regularScale.x, 1.5f * regularScale.y, 1.5f * regularScale.z); ;

    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
        swordCollider.transform.rotation = Quaternion.identity;
        swordCollider.size = regularBoxColliderSize;
        colherCollider.transform.localPosition = new Vector2(0, 0);
        transform.localScale = regularScale;
        if (playerController.spriteRenderer.flipX)
            transform.localPosition = rightAttackOffset;
        else
            transform.localPosition = leftAttackOffset;
    }

    void EndSpinAttack()
    {
        swordAnimator.SetBool("spinAttack", false);
        playerController.unLockMovement();
    }

    public void AddSpinTime(float time)
    {
        spinTime += time;
        upgradeMenu.ResumeGame();
    }

    public void StartAttack()
    {
        //playerController.LockMovement();
        if (swordAnimator.GetBool("spinAttack"))
        {
            SpinAttack();

            Invoke("EndSwordAttack", spinTime);
            Invoke("EndSpinAttack", spinTime);

        }
        else if (swordAnimator.GetBool("isMovingUp"))
        {
            AttackUp();
        }
        else if (swordAnimator.GetBool("isMovingDown"))
        {
            AttackDown();
        }
        else if (playerController.spriteRenderer.flipX)
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


}
