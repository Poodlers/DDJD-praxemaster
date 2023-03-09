using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    int collectibleCount = 0;

    int nextUpgradeThreshold = 1;

    public UpgradeMenu upgradeMenu;

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
        text.text = $"{collectibleCount} / {nextUpgradeThreshold}";
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

        collectibleCount++;


        if (collectibleCount == nextUpgradeThreshold)
        {
            upgradeMenu.PauseGame();
            nextUpgradeThreshold *= 3;
        }
        UpdateCount();

    }
}
