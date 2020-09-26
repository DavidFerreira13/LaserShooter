using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfiguration> waveConfigs = null;
    [SerializeField] bool loop = false;
    int startingWave = 0;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(spawnAllWaves());
        } 
        while (loop);
    }
    private IEnumerator spawnEnemiesInWave(WaveConfiguration wave)
    {
        for (int enemyCount = 0; enemyCount < wave.getNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
            wave.getEnemyPreab(),
            wave.getWaypoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyMovement>().setWaveConfig(wave);
            yield return new WaitForSeconds(wave.getTimeBetweenSpawns());
        }
    }
    private IEnumerator spawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return spawnEnemiesInWave(currentWave);

        }
    }
}
