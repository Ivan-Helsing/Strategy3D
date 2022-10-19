using System.Collections;
using System.Collections.Generic;
using TroopsAI;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAI : MonoBehaviour, ITroopsActions
{
    [SerializeField] HumanoidProperty humanoidProperty;
    [SerializeField] float secondsForFindNearestEnemy;

    Enemy currentEnemy;
    FindEnemies findEnemies;

    private float currentEnemyDistance = Mathf.Infinity;
    private float currentTargetDistance = Mathf.Infinity;
    private float attackRangeForCurrentTarget = 0f;

    [Tooltip("Stop Chasing Coroutine if false")]
    public bool isAnEnemy = true;



    public void StartAttack()
    {
        Debug.Log("ATTACK!!!");
    }
    public void StopAttack() 
    {
        //StopAttack
    }

    public void Die()
    {
        Debug.Log("DIE!!!");
    }

    public void StartChasing()
    {
        if (currentEnemy != null) 
        { 
            currentEnemy.OnThisEnemyDie -= StartChasing;
            StopCoroutine(ChaseNearestEnemy());
        }
        
        StartCoroutine(ChaseNearestEnemy());
    }

    public void StopChasing()
    {
        StopCoroutine(ChaseNearestEnemy());
    }

    IEnumerator ChaseNearestEnemy()
    {
        while (isAnEnemy)
        {
            TargetingNearestEnemy(findEnemies.CurrentEnemies());
            MaximumAttackDistance();
            RunToTarget();
            yield return new WaitForSeconds(secondsForFindNearestEnemy);
        }
    }


    private void TargetingNearestEnemy(Enemy[] currentEnemies)
    {
        if (findEnemies.CurrentEnemies().Length == 0) { isAnEnemy = false; return; }

        currentTargetDistance = Mathf.Infinity;
        foreach (Enemy enemy in currentEnemies)
        {
            if (!isAnEnemy) return;
            currentEnemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (currentEnemyDistance < currentTargetDistance)
            {
                if (!isAnEnemy) return;
                currentTargetDistance = currentEnemyDistance;
                currentEnemy = enemy;
                currentEnemy.OnThisEnemyDie += StartChasing;
            }
        }
    }

    public void RunToTarget()
    {
        if (!isAnEnemy) return;
        GetComponent<NavMeshAgent>().SetDestination(currentEnemy.gameObject.transform.position);
    }

    private void MaximumAttackDistance()
    {
        if (!isAnEnemy) return;
        attackRangeForCurrentTarget = humanoidProperty.BasicAttackRange + GetComponent<CapsuleCollider>().radius + currentEnemy.gameObject.GetComponent<CapsuleCollider>().radius;
        if (Vector3.Distance(transform.position, currentEnemy.gameObject.transform.position) > attackRangeForCurrentTarget)
        {
            if (!isAnEnemy) return;
            GetComponent<NavMeshAgent>().stoppingDistance = attackRangeForCurrentTarget;
        }
    }

    void Start()
    {
        findEnemies = FindObjectOfType<FindEnemies>();
        StartChasing();
        //Attack();
        // Die();
    }

    void Update()
    {
        if (!isAnEnemy) { StopChasing(); }
    }
}
