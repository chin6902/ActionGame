using ActionGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private float innerSpawnRadius = 5f;
    [SerializeField] private float outerSpawnRadius = 10f;
    [SerializeField] private int meteorCount = 5; 
    [SerializeField] private float waveInterval = 1f;
    [SerializeField] private EnemyStateMachine enemyStateMachine;

    private List<GameObject> spawnedMeteors = new List<GameObject>();

    private void Start()
    {
        enemyStateMachine.OnStartScream += StartWaveSequence;
        enemyStateMachine.OnStopScream += ClearMeteors;
    }

    private void OnDestroy()
    {
        enemyStateMachine.OnStartScream -= StartWaveSequence;
        enemyStateMachine.OnStopScream -= ClearMeteors;
    }

    private void StartWaveSequence()
    {
        StartCoroutine(SpawnMeteorWaves());
    }

    private IEnumerator SpawnMeteorWaves()
    {
        yield return new WaitForSeconds(0.6f);

        // Spawn the first wave
        SpawnMeteors(innerSpawnRadius, 0f);
        yield return new WaitForSeconds(waveInterval);

        // Clear the first wave
        ClearMeteors();

        // Spawn the second wave with an offset
        SpawnMeteors(outerSpawnRadius, 360f / (2 * meteorCount));
    }

    private void SpawnMeteors(float radius, float angleOffset)
    {
        ClearMeteors(); // Clear any existing meteors before spawning new ones

        float angleStep = 360f / meteorCount;

        for (int i = 0; i < meteorCount; i++)
        {
            // Add the offset to each meteor's angle
            float angle = (i * angleStep + angleOffset) * Mathf.Deg2Rad;

            Vector3 spawnPosition = new Vector3(
                transform.position.x + radius * Mathf.Cos(angle),
                transform.position.y,
                transform.position.z + radius * Mathf.Sin(angle)
            );

            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
            spawnedMeteors.Add(meteor);
        }
    }

    private void ClearMeteors()
    {
        foreach (GameObject meteor in spawnedMeteors)
        {
            if (meteor != null)
            {
                Destroy(meteor);
            }
        }
        spawnedMeteors.Clear();
    }
}
