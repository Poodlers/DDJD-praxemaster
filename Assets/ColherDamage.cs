using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColherDamage : MonoBehaviour
{
    public int colherDamage = 1;
    public UpgradeMenu upgradeMenu;


    public void colherDamageUp()
    {
        colherDamage++;
        upgradeMenu.ResumeGame();

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
                enemyController.TakeDamage(colherDamage);
            }
        }
    }
}
