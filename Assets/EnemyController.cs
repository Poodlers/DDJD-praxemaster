using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Animator animator;
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public float health = 1;
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");

    }

    public void RemoveEntity()
    {
        Destroy(gameObject);
    }

}
