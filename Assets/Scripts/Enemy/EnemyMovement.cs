using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float moveSpeed;
    [SerializeField] int damage = 1;

    List<WayPoint> path = new List<WayPoint>();
    PathFinder pathFinder;

    [SerializeField] ParticleSystem castleParticle;

    EnemyDamage enemyDamage;

    Castle castle;

    Vector3 targetPosition;


    void Start()
    {
        castle = FindObjectOfType<Castle>();
        enemyDamage = GetComponent<EnemyDamage>();
        pathFinder = FindObjectOfType<PathFinder>();
        path = pathFinder.GetPath();
        StartCoroutine(EnemyMove());
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime* moveSpeed);
    }
    IEnumerator EnemyMove()
    {
        foreach (WayPoint waypoint in path)
        {
            transform.LookAt(waypoint.transform);
            targetPosition = waypoint.transform.position;
            yield return new WaitForSeconds(speed);
            
        }

        castle.DamageCastle(damage);
        enemyDamage.DestroyEnemy(castleParticle);
        
    }
}
