using UnityEngine;
using System;

public class FinoCollectible : MonoBehaviour
{

    public static event Action onCollectibleCollected;

    public static int totalCollectibles;
    public float frequency = 1f;
    public float amplitude = 0.01f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Awake()
    {
        posOffset = transform.position;
        totalCollectibles++;
    }
    // Update is called once per frame
    void Update()
    {

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
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
