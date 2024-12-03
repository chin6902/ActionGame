using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner1 : MonoBehaviour
{
    public GameObject enemyPrefabType1;
    public GameObject enemyPrefabType2;
    public int maxEnemies = 10;
    public float spawnRadius = 20f;
    public float spawnInterval = 5f;
    public float minSpawnDistance = 5f;

    private List<GameObject> enemies = new List<GameObject>();
    private int enemyTypeToggle = 0; 

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemies), 0f, spawnInterval);
    }

    private void SpawnEnemies()
    {
        enemies.RemoveAll(enemy => enemy == null);

        while (enemies.Count < maxEnemies)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            if (spawnPosition != Vector3.zero)
            {
                GameObject newEnemy = Instantiate(GetNextEnemyType(), spawnPosition, Quaternion.identity);
                enemies.Add(newEnemy);
            }
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 balancedPosition = GetBalancedNavMeshPosition();
            if (balancedPosition != Vector3.zero && IsPositionValid(balancedPosition))
            {
                return balancedPosition;
            }
        }
        return Vector3.zero;
    }

    private Vector3 GetBalancedNavMeshPosition()
    {
        float angle = Random.Range(0, 360);
        float distance = Random.Range(0.5f * spawnRadius, spawnRadius);

        float xOffset = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
        float zOffset = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

        Vector3 randomPoint = transform.position + new Vector3(xOffset, 0, zOffset);
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(position, enemy.transform.position) < minSpawnDistance)
            {
                return false;
            }
        }
        return true;
    }

    private GameObject GetNextEnemyType()
    {
        enemyTypeToggle = 1 - enemyTypeToggle;
        return enemyTypeToggle == 0 ? enemyPrefabType1 : enemyPrefabType2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
