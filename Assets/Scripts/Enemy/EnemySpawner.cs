using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Range(0.1f, 20f)] float spawnInterval;
    
    [SerializeField] EnemyMovement enemy1;
    [SerializeField] EnemyMovement enemy2;

    private EnemyMovement enemyPrefab;

    [SerializeField] AudioClip enemySpawnSoundFX;


    void Start()
    {
 
        StartCoroutine(EnemySpawn());

    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            float variant = Random.Range(0, 10);

            if (variant <= 5)
                enemyPrefab = enemy1;
            else
                enemyPrefab = enemy2;

            var newEnemy = Instantiate(enemyPrefab,transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;

            GetComponent<AudioSource>().PlayOneShot(enemySpawnSoundFX);

            yield return new WaitForSeconds(spawnInterval);
            
        }
        
    }
}
