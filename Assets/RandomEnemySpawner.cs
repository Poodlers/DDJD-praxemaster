using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    PlayerController playerController;
    float minimumY = -1f;
    float maximumY = 1f;
    float minimumX = -2f;
    float maximumX = 2f;
    const int frameRate = 50;
    public int secondsBetweenSpawn = 3;

    int framesCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isDefeated || UpgradeMenu.GameIsPaused) return;

        if (framesCount >= secondsBetweenSpawn * frameRate)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Vector3 randomSpawnPoint = new Vector3(Random.Range(minimumX, maximumX), Random.Range(minimumY, maximumY), 0);
            Instantiate(enemyPrefabs[randomEnemy], randomSpawnPoint, Quaternion.identity);
            framesCount = 0;
        }
        else
        {
            framesCount++;
        }

    }
}
