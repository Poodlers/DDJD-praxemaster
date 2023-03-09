using UnityEngine;
using System;

public class FinoCollectible : MonoBehaviour
{

    public static event Action onCollectibleCollected;

    public static int totalCollectibles;
    public float speed = 0.05f;
    public float amplitude = 0.01f;

    void Awake()
    {
        totalCollectibles++;
    }
    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed);
        transform.localPosition = new Vector2(transform.localPosition.x, newY * amplitude);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            onCollectibleCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
