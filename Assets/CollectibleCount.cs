using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    int collectibleCount = 0;

    void Start()
    {
        UpdateCount();
        onEnable();
    }
    void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();

    }

    void UpdateCount()
    {
        text.text = $"{collectibleCount} / {FinoCollectible.totalCollectibles}";
    }

    void onEnable()
    {
        FinoCollectible.onCollectibleCollected += onCollectibleCollected;

    }

    void OnDisable()
    {
        FinoCollectible.onCollectibleCollected -= onCollectibleCollected;
    }
    void onCollectibleCollected()
    {
        Debug.Log("Collectible collected");
        collectibleCount++;
        UpdateCount();
    }
}
