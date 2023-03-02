using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    void Start()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos.x);
        Debug.Log(mousePos.y);
    }

    public float ProjectileSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.position);
        transform.position += transform.right * Time.deltaTime * ProjectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }

}
