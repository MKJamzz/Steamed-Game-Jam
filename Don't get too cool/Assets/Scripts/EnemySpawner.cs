using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;   // Assign your enemy prefab here

    [Header("Spawn Settings")]
    public float spawnInterval = 2f; // Time in seconds between spawns
    public Transform[] spawnPoints;  // Optional: Array of possible spawn points

    private void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);

            // Determine the spawn position:
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = transform.rotation;

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                // If you have multiple spawn points, choose one at random
                int randomIndex = Random.Range(0, spawnPoints.Length);
                spawnPosition = spawnPoints[randomIndex].position;
                spawnRotation = spawnPoints[randomIndex].rotation;
            }

            // Instantiate (spawn) the enemy
            Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        }
    }
}
