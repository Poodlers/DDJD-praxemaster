using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{

    Animator animator;
    public bool isInvincible = false;

    public GameObject finoPrefab;

    CapsuleCollider2D capsuleCollider2D;

    public float invincibilityTime = 0.2f;
    public AIPath aiPath;
    public float Health
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
            }
        }
        get
        {
            return health;
        }
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

    }


    public float health = 2;
    public void TakeDamage(float damage)
    {
        isInvincible = true;
        Health -= damage;

    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibilityTime -= Time.deltaTime;
            if (invincibilityTime <= 0)
            {
                isInvincible = false;
                invincibilityTime = 0.2f;
            }
        }

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void Damaged()
    {
        animator.SetTrigger("Damaged");
    }

    public void Defeated()
    {

        aiPath.canMove = false;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        capsuleCollider2D.enabled = false;

        //destroy self after 3 seconds
        Destroy(gameObject, 3f);
        Instantiate(finoPrefab, transform.position, Quaternion.identity);

    }


}
