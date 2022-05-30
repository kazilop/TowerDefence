using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class Tower : MonoBehaviour
{
    [SerializeField] Transform towerTop;
    [SerializeField] Transform targetEnemy;

    [SerializeField] float shootdistance;
    [SerializeField] ParticleSystem bulletsParticle;
 //   [SerializeField] ParticleSystem smoke;

    private float yCorrection = 3f;
    Transform enemyPos;


    void Update()
    {
        SetTargetEnemy();

        if (targetEnemy)
        {
            LookAtEnemy();
            Fire();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length == 0) return;

        Transform closestEnemy = sceneEnemies[0].transform;
        foreach(EnemyDamage test in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy.transform, test.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform position1, Transform position2)
    {
        if(Vector3.Distance(transform.position, position1.position) < 
            Vector3.Distance(transform.position, position2.position))
        {
            return position1;
        }
        else
        {
            return position2;
        }
    }

    private void LookAtEnemy()
    {
        var enemyPos = targetEnemy.transform.position;
        enemyPos.y = enemyPos.y + yCorrection;
        
        towerTop.LookAt(enemyPos);
      
    }



    private void Fire()
    {
        
            float distanceToEnemy = Vector3.Distance(targetEnemy.position, gameObject.transform.position);
            if (distanceToEnemy <= shootdistance)
            {
                Shoot(true);
            }
            else
            {
                Shoot(false);
            }
        
        
    }

    private void Shoot(bool shoot)
    {
        var emission = bulletsParticle.emission;
     //   var emissionSmoke = smoke.emission;
        emission.enabled = shoot;
     //   emissionSmoke.enabled = shoot;

    }
}
