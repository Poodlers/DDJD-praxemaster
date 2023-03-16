using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float[] enemyRarity;

    float enemyRaritiesSum;

    PlayerController playerController;
    public float minimumY;
    public float maximumY;
    public float minimumX;
    public float maximumX;

    public float secondsBetweenSpawn = 3f;

    float enemySpawnCounter = 0;

    float secondsCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyRaritiesSum = GetEnemyRaritySum();

    }
    private float GetEnemyRaritySum()
    {
        float sum = 0;
        for (int i = 0; i < enemyRarity.Length; ++i)
            sum += enemyRarity[i];
        return sum;
    }

    private GameObject GetSpawningEnemy()
    {

        float random = Random.Range(0, enemyRaritiesSum);

        for (int i = 0; i < enemyPrefabs.Length; ++i)
        {
            if (random <= enemyRarity[i])
            {

                return enemyPrefabs[i];
            }
            random -= enemyRarity[i];
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isDefeated || UpgradeMenu.GameIsPaused) return;

        secondsCounter += Time.deltaTime;
        if (enemySpawnCounter >= secondsBetweenSpawn)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Vector3 randomSpawnPoint = new Vector3(Random.Range(minimumX, maximumX), Random.Range(minimumY, maximumY), 0);
            GameObject newEnemy = Instantiate(GetSpawningEnemy(), randomSpawnPoint, Quaternion.identity);
            //make newEnemy health scale with secondsCounter
            newEnemy.GetComponent<EnemyController>().health = (int)(newEnemy.GetComponent<EnemyController>().health * (secondsCounter / 10));
            enemySpawnCounter = 0;
        }
        else
        {
            enemySpawnCounter += Time.deltaTime;
        }

    }
}
