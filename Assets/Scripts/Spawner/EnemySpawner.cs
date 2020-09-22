using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemies;

    private readonly float waveTimer = 10f;
    private float timeTillWave = 0f;
    private bool waveSpawn;
    private int createdEnemy;

    private void FixedUpdate()
    {
        // Increases the timer to allow the timed waves to work
        timeTillWave += Time.deltaTime;
        if (waveSpawn)
        {
            //spawns an enemy
            spawnEnemy();
            waveSpawn = false;
        }
        // checks if the time is equal to the time required for a new wave
        if (timeTillWave >= waveTimer)
        {
            // enables the wave spawner
            waveSpawn = true;
            // sets the time back to zero
            timeTillWave = 0.0f;
        }
        //StartCoroutine(waitForSpawn());
    } 

    /*IEnumerator createEnemy()
    {
        yield return new WaitForSeconds(5f);
    }*/

    void spawnEnemy()
    {
        if(createdEnemy <= 5)
        {
            Instantiate(enemies, this.transform.position, this.transform.rotation);
            PlayerManager.Instance.enemyDetail.Add(enemies); 
        }
        ++createdEnemy;
    }
}
