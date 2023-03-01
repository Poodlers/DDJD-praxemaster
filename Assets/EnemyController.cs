using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{

    Animator animator;

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

    }


    public float health = 2;
    public void TakeDamage(float damage)
    {
        Health -= damage;

    }

    private void Update()
    {
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
        animator.SetTrigger("Defeated");

    }


}
