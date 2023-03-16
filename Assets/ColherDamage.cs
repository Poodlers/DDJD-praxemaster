using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColherDamage : MonoBehaviour
{
    public int colherDamage = 1;
    public UpgradeMenu upgradeMenu;

    public float knockbackForce = 0.5f;


    public void colherDamageUp()
    {
        colherDamage++;
        upgradeMenu.ResumeGame();

    }

    public void knockbackForceUp()
    {
        knockbackForce += 0.5f;
        upgradeMenu.ResumeGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController =
             collision.gameObject.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.TakeDamage(colherDamage);
                enemyController.ApplyKnockback(transform.position, knockbackForce);
            }
        }
    }
}
