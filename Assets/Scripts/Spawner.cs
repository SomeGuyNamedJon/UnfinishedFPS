using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Update is called once per frame
    public GameObject[] enemiesEasy;
    public GameObject[] enemiesMedium;
    public GameObject[] enemiesHard;

    private Transform spawnPoint;
    private int index;

    void Start(){
        StartCoroutine(BeginSpawner());
    }

    IEnumerator BeginSpawner()
    {
        if(!GameManager.gameOver && GameManager.enemiesSpawned < GameManager.enemyMaxCount){
            Spawn();
        }

        yield return new WaitForSeconds(GameManager.enemySpawnModifier);
    }

    void Spawn(){
        //spawnPoint = transform;
        spawnPoint.position = transform.position;
        if(GameManager.levelCnt > 4){
            index = Random.Range(0,(enemiesEasy.Length - 1));
            Instantiate(enemiesEasy[index], spawnPoint);

            spawnPoint.position = new Vector3(spawnPoint.position.x + 1f, spawnPoint.position.y, spawnPoint.position.z);
            
            index = Random.Range(0,(enemiesEasy.Length - 1));
            Instantiate(enemiesEasy[index], spawnPoint);

            GameManager.enemiesSpawned += 2;
        }
        if(GameManager.levelCnt > 9){
            index = Random.Range(0,(enemiesMedium.Length - 1));
            Instantiate(enemiesMedium[index], spawnPoint);

            spawnPoint.position = new Vector3(spawnPoint.position.x + 1f, spawnPoint.position.y, spawnPoint.position.z);
            
            index = Random.Range(0,(enemiesMedium.Length - 1));
            Instantiate(enemiesMedium[index], spawnPoint);

            spawnPoint.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + 1f);
            
            index = Random.Range(0,(enemiesMedium.Length - 1));
            Instantiate(enemiesMedium[index], spawnPoint);
            GameManager.enemiesSpawned += 3;
        }
        if(GameManager.levelCnt > 14){
            index = Random.Range(0,(enemiesHard.Length - 1));
            Instantiate(enemiesHard[index], spawnPoint);

            spawnPoint.position = new Vector3(spawnPoint.position.x + 1f, spawnPoint.position.y, spawnPoint.position.z);
            
            index = Random.Range(0,(enemiesHard.Length - 1));
            Instantiate(enemiesHard[index], spawnPoint);

            spawnPoint.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + 1f);
            
            index = Random.Range(0,(enemiesHard.Length - 1));
            Instantiate(enemiesHard[index], spawnPoint);

            spawnPoint.position = new Vector3(spawnPoint.position.x - 1f, spawnPoint.position.y, spawnPoint.position.z);
            
            index = Random.Range(0,(enemiesHard.Length - 1));
            Instantiate(enemiesHard[index], spawnPoint);

            spawnPoint.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z - 2f);
            
            index = Random.Range(0,(enemiesHard.Length - 1));
            Instantiate(enemiesHard[index], spawnPoint);
            GameManager.enemiesSpawned += 5;
        }
    }
}
