using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Animator animator;

    public float moveSpeed = 0.1f;
    GameObject player;
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
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 playerPosition = player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
    }

    public float health = 2;
    public void TakeDamage(float damage)
    {
        Health -= damage;

    }
    public void Damaged()
    {
        animator.SetTrigger("Damaged");
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
