using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private Vector3 mousePos;

    private Camera mainCamera;
    private Rigidbody2D rb;

    public float force;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Coffee hit " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController =
             collision.gameObject.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.TakeDamage(1);

            }
            Destroy(gameObject);
        }
    }

}