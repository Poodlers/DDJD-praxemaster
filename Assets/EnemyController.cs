using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{

    Animator animator;

    Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        health = maxHealth;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

    }

    IEnumerator ApplyBurn(int burnTicks, float burnDamage)
    {
        for (int i = 0; i < burnTicks; i++)
        {
            Health -= burnDamage;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ApplyKnockback(Vector3 sourcePosition, float knockbackForce)
    {
        if (health <= 0) return;
        aiPath.enabled = false;
        Vector2 difference = (transform.position - sourcePosition).normalized;
        Vector2 force = difference * knockbackForce;
        rb.AddForce(force, ForceMode2D.Impulse);

        StartCoroutine(ResetKnockback());

    }

    IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.5f);
        if (health > 0) aiPath.enabled = true;
    }

    public void Burn(int burnTicks, float burnDamage)
    {
        StartCoroutine(ApplyBurn(burnTicks, burnDamage));
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

        aiPath.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        capsuleCollider2D.enabled = false;

        Instantiate(finoPrefab, transform.position, Quaternion.identity);
        //destroy self after 3 seconds
        Destroy(gameObject, 3f);


    }


}
