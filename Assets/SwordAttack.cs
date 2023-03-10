using System.Collections;
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
    float spinAnimationTime;
    float currentSpinTime;
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
        AnimationClip[] clips = swordAnimator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "spin_attack":
                    spinAnimationTime = clip.length;
                    currentSpinTime = spinAnimationTime;

                    break;
                default:
                    break;
            }
        }
    }

    public void UpgradeColherRange()
    {

        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        regularScale = transform.localScale;

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
        colherCollider.transform.Rotate(0, 0, -90);
        transform.localPosition = new Vector3(-0.04f, -0.16f, 0);
    }
    public void SpinAttack()
    {
        swordCollider.enabled = true;
        swordCollider.size = new Vector2(regularBoxColliderSize.x * 2.5f, regularBoxColliderSize.y * 2.5f);
        swordCollider.transform.localPosition = new Vector3(-0.1f, 0, 0);
        colherGFX.transform.localPosition = new Vector3(-0.1f, 0, 0);
        transform.localScale = new Vector3(1.5f * regularScale.x, 1.5f * regularScale.y, 1.5f * regularScale.z);

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

    public IEnumerator EndSpinAttackAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        swordAnimator.SetBool("spinAttack", false);
        EndSwordAttack();
    }


    public void AddSpinTime()
    {
        Debug.Log("spinAnimationTime" + spinAnimationTime);
        currentSpinTime += spinAnimationTime;
        Debug.Log("currentSpinTime" + currentSpinTime);
        upgradeMenu.ResumeGame();
    }

    public void unlockPlayerMovement()
    {
        playerController.unLockMovement();
    }

    public void StartAttack()
    {
        //playerController.LockMovement();

        if (swordAnimator.GetBool("spinAttack"))
        {

            SpinAttack();
            Debug.Log("currentSpiTime" + currentSpinTime);
            StartCoroutine(EndSpinAttackAfterTime(currentSpinTime - 0.1f));

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
        unlockPlayerMovement();
    }


}
