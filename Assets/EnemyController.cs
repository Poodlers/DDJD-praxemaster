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

    public float maxHealth = 5;
    public float health;
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
        health = maxHealth;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

    }




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
                invincibilityTime = 0.1f;
            }
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
        Instantiate(finoPrefab, transform.localPosition, Quaternion.identity);
        //destroy self after 3 seconds
        Destroy(gameObject, 3f);


    }


}
