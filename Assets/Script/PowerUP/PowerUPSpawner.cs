using System.Collections.Generic;
using UnityEngine;

public class PowerUPSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            SpawnPowerUp();
            Debug.Log("Power-up spawned!");
        }

    }
    public void SpawnPowerUp()
    {
        int randomNumber = 0;

        float chance = Random.Range(0f, 100f);

        if (chance <= 5f)
        {
            randomNumber = 2;
        }

        else if (chance <= 35f)
        {
            randomNumber = 1;
        }

        List<Vector2Int> usedPositions =
            new List<Vector2Int>();

        for (int i = 0; i < randomNumber; i++)
        {
            int randomIndex =
                Random.Range(0, powerUpPrefabs.Length);

            int randomX;
            int randomZ;

            Vector2Int positionKey;

            do
            {
                randomX = Random.Range(0, 3);
                randomZ = Random.Range(10, 70);

                positionKey =
                    new Vector2Int(randomX, randomZ);

            } while (usedPositions.Contains(positionKey));

            usedPositions.Add(positionKey);

            Vector3 spawnPosition =
                new Vector3(randomX, 1.2f, randomZ);

            Instantiate(
                powerUpPrefabs[randomIndex],
                spawnPosition,
                Quaternion.identity
            );
        }
    }
}