using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    public Collider2D swordCollider;
    Vector2 rightAttackOffset;

    private void Start()
    {

        rightAttackOffset = transform.position;
    }


    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;

    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(-rightAttackOffset.x,
        rightAttackOffset.y);


    }
    public void AttackUp()
    {
        swordCollider.enabled = true;
        transform.Rotate(0, 0, 90);
        transform.localPosition = new Vector2(0,
        0);


    }

    public void AttackDown()
    {
        swordCollider.enabled = true;
        transform.Rotate(0, 0, 90);
        transform.localPosition = new Vector2(0,
        -0.16f);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;

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
