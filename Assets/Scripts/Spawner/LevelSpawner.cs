using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public int controlLvl;

    private int currentLevel;
    //private PlayerMovement crntplayer;
    private GameObject lvl;
    private int spawnLevelStop;
    /*
    void Start()
    { 
        crntplayer = PlayerManager.currentPlayer.GetComponent<PlayerMovement>();  
    }
     */
    void FixedUpdate()
    {
        controlLvl = PlayerManager.Instance.MovePlayer.cntrl;

        if (controlLvl >= 6 && spawnLevelStop <= 3)
        {
            SpawnLevel();
            transform.Translate(new Vector3(50f, 0f, 0f));
        }

        if (controlLvl == 0)
        {
            spawnLevelStop = 0; 
        }
    }

    void SpawnLevel()
    {
        //Random.seed = System.DateTime.Now.Millisecond;
        ++spawnLevelStop;
        Random.InitState(System.DateTime.Now.Millisecond);
        currentLevel = Random.Range(0, 11);
        lvl = PlayerManager.Instance.levels[currentLevel];
        int randomSpawnSelector = Random.Range(1, 3);
         
        var clone = Instantiate(lvl, this.transform.position + new Vector3(25f, 0f, 0f), this.transform.rotation);
        if (randomSpawnSelector == 1 && spawnLevelStop == 1)
        {
            Debug.Log("Generated1");
            var spawnClone = clone.transform.GetChild(0).gameObject;
            spawnClone.SetActive(true);
            randomSpawnGenerator(spawnClone.transform);
        }
        if (randomSpawnSelector == 2 && spawnLevelStop == 2)
        {
            Debug.Log("Generated2");
            var spawnClone = clone.transform.GetChild(0).gameObject;
            spawnClone.SetActive(true);
            randomSpawnGenerator(spawnClone.transform);
        } 
    }

    void randomSpawnGenerator(Transform position)
    {
        int randomSpawnGenerator = Random.Range(1, 4);

        if(PlayerManager.Instance.playerShoots.totalAmmo == 0)
        {
            int randomSpawnGenerator2 = Random.Range(0,2);
            if (randomSpawnGenerator2 == 0)
                randomSpawnGenerator = 1;
            else
                randomSpawnGenerator = 3;
        }
        if (randomSpawnGenerator == 1)
        {
            Debug.Log("Generatedhealth");
            var healthClone = Instantiate(PlayerManager.Instance.healthSpawn, position.position, position.rotation);
            healthClone.name = PlayerManager.Instance.healthSpawn.name;
        }
        else if (randomSpawnGenerator == 2)
        {
            Debug.Log("Generatedammo"); 
            var ammoClone = Instantiate(PlayerManager.Instance.ammoSpawn, position.position, position.rotation);
            ammoClone.name = PlayerManager.Instance.ammoSpawn.name;
        }
        else
        {
            Debug.Log("Generatedweapon");
            int randomweaponGenerator = Random.Range(0, 7);
            var weaponClone = Instantiate(PlayerManager.Instance.weaponsList[randomweaponGenerator],position.position, position.rotation);
            weaponClone.name = PlayerManager.Instance.weaponsList[randomweaponGenerator].name;
        } 
    }
}
