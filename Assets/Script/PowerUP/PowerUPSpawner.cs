using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the spawning of power-ups at random positions within the game world.
/// When the player enters the trigger area, a random number of power-ups (between 0 and 2) 
/// will be spawned at random positions within a specified range.
/// The chance of spawning 0, 1, or 2 power-ups is determined by a random number generator, 
/// with a 5% chance for 2 power-ups, a 30% chance for 1 power-up, and a 65% chance for no power-ups.
/// The power-ups are instantiated from a predefined array of power-up prefabs, and their 
/// positions are generated to ensure that they do not overlap with each other.
/// </summary>

public class PowerUPSpawner : MonoBehaviour
{
    // Array of power-up prefabs that can be spawned
    public GameObject[] powerUpPrefabs;

    // This method is called when another collider enters the 
    // trigger collider attached to the same GameObject as this script with tag "Player". 
    // It checks if the colliding object has the tag "Player", and if so, it calls the 
    // SpawnPowerUp method to spawn power-ups at random positions.
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            SpawnPowerUp();
        }

    }

    // This method is responsible for spawning power-ups at random positions within the game world.
    // It first generates a random number to determine how many power-ups to spawn (0, 1, or 2) 
    // based on predefined chances.
    // It then uses a loop to spawn the determined number of power-ups. For each power-up, it 
    // randomly selects a prefab from the powerUpPrefabs array and generates random x and z 
    // coordinates for the spawn position.
    // To ensure that power-ups do not overlap, it keeps track of used positions in a List 
    // of Vector2Int. If a generated position has already been used, it will generate new 
    // coordinates until it finds an unused position.
    public void SpawnPowerUp()
    {
        int randomNumber = 0;

        float chance = Random.Range(0f, 101f);

        if (chance <= 10f)
        {
            randomNumber = 2;
        }

        else if (chance <= 30f)
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