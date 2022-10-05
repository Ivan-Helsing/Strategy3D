using System.Collections;
using System.Collections.Generic;
using TroopsAI;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAI : MonoBehaviour, ITroopsActions
{
    [SerializeField] HumanoidProperty humanoidProperty;
    [SerializeField] float secondsForFindNearestEnemy;

    GameObject currentTarget;
    GameObject lastTarget;

    private float currentEnemyDistance = Mathf.Infinity;
    private float currentTargetDistance = Mathf.Infinity;
    private float attackRangeForCurrentTarget = 0f;

    bool isAnyEnemy = true;

    public void Attack()
    {
        Debug.Log("ATTACK!!!");
    }

    public void Die()
    {
        Debug.Log("DIE!!!");
    }

    public void StartChasing()
    {
        StartCoroutine(ChaseNearestEnemy());
        //chasingNow = !chasingNow;
    }
    public void StopChasing()
    {
        StopCoroutine(ChaseNearestEnemy());
    }

    // public void ChaseNearestEnemyCoroutine()
    //{
    //    if (chasingNow)
    //    {
    //        StartCoroutine(ChaseNearestEnemy());
    //        coroutineIsRunning = true;
    //    }
    //    else
    //    {
    //        if (!coroutineIsRunning) { return; }
    //        StopCoroutine(ChaseNearestEnemy());
    //        coroutineIsRunning = false;
    //    }
    //}

    IEnumerator ChaseNearestEnemy()
    {
        while (isAnyEnemy)
        {
            CurrentTarget();
            MaximumAttackDistance();
            RunToTarget();
            yield return new WaitForSeconds(secondsForFindNearestEnemy);
        }        
    }

    public void CurrentTarget()
    {        
        Enemy[] currentEnemies = FindObjectsOfType<Enemy>();
        if (currentEnemies.Length == 0) { isAnyEnemy = false; return; }


        currentTargetDistance = Mathf.Infinity;
        foreach (var enemy in currentEnemies)
        {
            currentEnemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (currentEnemyDistance < currentTargetDistance)
            {
                currentTargetDistance = currentEnemyDistance;
                currentTarget = enemy.CurrentObject;
            }
        }
    }

    public void RunToTarget()
    {
        if (!isAnyEnemy) { return; }
        GetComponent<NavMeshAgent>().SetDestination(currentTarget.transform.position);
    }

    private void MaximumAttackDistance()
    {
        if (!isAnyEnemy) { return; }
        attackRangeForCurrentTarget = humanoidProperty.BasicAttackRange + GetComponent<CapsuleCollider>().radius + currentTarget.GetComponent<CapsuleCollider>().radius;
        if (Vector3.Distance(transform.position, currentTarget.transform.position) > attackRangeForCurrentTarget)
        {
            GetComponent<NavMeshAgent>().stoppingDistance = attackRangeForCurrentTarget;
        }
    }

    void Start()
    {
        //RunToTarget();
        StartChasing();
        //Attack();
        // Die();
    }

    void Update()
    {
        if (!isAnyEnemy) { StopChasing(); }
    }
    void FixedUpdate()
    {
        
        
      


    }


    




}
