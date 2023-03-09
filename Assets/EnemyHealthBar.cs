using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 localScale;
    EnemyController enemyController;

    void Start()
    {
        localScale = transform.localScale;
        enemyController = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = (enemyController.health / enemyController.maxHealth);
        transform.localScale = localScale;

    }
}
