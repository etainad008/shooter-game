using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeBetweenSpawns;
    private float nextTimeToSpawn = 0;

    public Transform[] spawnPoints;
    public Enemy enemy;

    private void Update()
    {
        //check if its time to spawn
        if(Time.time > nextTimeToSpawn)
        {
            EnemySpawn();

            nextTimeToSpawn = Time.time + timeBetweenSpawns;
        }
    }

    void EnemySpawn()
    {
        Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
    }
}
