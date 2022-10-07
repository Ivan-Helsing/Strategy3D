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
            //FindEnemies();
            TargetingNearestEnemy(findEnemies.CurrentEnemies());
            MaximumAttackDistance();
            RunToTarget();
            yield return new WaitForSeconds(secondsForFindNearestEnemy);
        }
    }

    //public void FindEnemies()
    //{
    //    if (findEnemies.CurrentEnemies.Length == 0) { isAnEnemy = false; return; }

    //    Enemy[] currentEnemies = FindObjectsOfType<Enemy>();
    //    if (currentEnemies.Length == 0) { isAnEnemy = false; return; }

    //    //TargetingNearestEnemy(currentEnemies);
    //    //MaximumAttackDistance();
    //    //RunToTarget();
    //}

    private void TargetingNearestEnemy(Enemy[] currentEnemies)
    {
        if (findEnemies.CurrentEnemies().Length == 0) { isAnEnemy = false; return; }

        currentTargetDistance = Mathf.Infinity;
        foreach (Enemy enemy in currentEnemies)
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
        GetComponent<NavMeshAgent>().SetDestination(currentTarget.transform.position);
    }

    private void MaximumAttackDistance()
    {
        attackRangeForCurrentTarget = humanoidProperty.BasicAttackRange + GetComponent<CapsuleCollider>().radius + currentTarget.GetComponent<CapsuleCollider>().radius;
        if (Vector3.Distance(transform.position, currentTarget.transform.position) > attackRangeForCurrentTarget)
        {
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
