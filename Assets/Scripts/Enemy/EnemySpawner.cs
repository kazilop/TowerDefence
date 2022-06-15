using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Range(0.1f, 20f)] float spawnInterval;
    [SerializeField] EnemyMovement enemyPrefab;

    [SerializeField] AudioClip enemySpawnSoundFX;


    void Start()
    {
 
        StartCoroutine(EnemySpawn());

    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            

            var newEnemy = Instantiate(enemyPrefab,transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;

            GetComponent<AudioSource>().PlayOneShot(enemySpawnSoundFX);

            yield return new WaitForSeconds(spawnInterval);
            
        }
        
    }
}
