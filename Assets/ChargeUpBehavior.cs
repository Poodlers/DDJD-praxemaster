using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeUpBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 mousePos;

    private Transform playerTransform;

    public UpgradeMenu upgradeMenu;
    private Camera mainCamera;
    public float chargeUpTime = 1f;
    void Start()
    {

        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    public void UpgradeChargeUpTime(float time)
    {
        chargeUpTime += time;
        upgradeMenu.ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        transform.position = playerTransform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        chargeUpTime -= Time.deltaTime;
        if (chargeUpTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Coffee chargeup hit " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController =
             collision.gameObject.GetComponent<EnemyController>();

            if (enemyController != null && enemyController.isInvincible == false)
            {
                enemyController.TakeDamage(1);
            }
        }
    }
}
