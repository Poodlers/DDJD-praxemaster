
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
        float scale_x = (enemyController.health / enemyController.maxHealth) < 0 ? 0 : (enemyController.health / enemyController.maxHealth);
        localScale.x = scale_x;
        transform.localScale = localScale;

    }
}
