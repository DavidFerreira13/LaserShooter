using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyWaveConfiguration")]
public class WaveConfiguration : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] GameObject pathPrefab = null;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] int numberOfEnemies = 10;

    public GameObject getEnemyPreab() { return enemyPrefab; }
    public GameObject getPathPreab() { return enemyPrefab; }
    public List<Transform> getWaypoints()
    {
        var waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float getTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float getMovementSpeed() { return movementSpeed; }
    public float getSpawnRandomFactor() { return spawnRandomFactor; }
    public int getNumberOfEnemies() { return numberOfEnemies; }
}
