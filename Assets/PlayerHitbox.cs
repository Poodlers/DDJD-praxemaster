using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{

    PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (playerController.isInvincible) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerController.TakeDamage(1);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


    }
}
