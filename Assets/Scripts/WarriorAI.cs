using System.Collections;
using System.Collections.Generic;
using TroopsAI;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAI : MonoBehaviour, ITroopsActions
{
    [SerializeField] HumanoidProperty humanoidProperty;

    GameObject currentTarget;
    GameObject lastTarget;

    private float currentEnemyDistance = Mathf.Infinity;
    private float attackRangeForCurrentTarget = 0f;


    public void Attack()
    {
        Debug.Log("ATTACK!!!");
    }

    public void Die()
    {
        Debug.Log("DIE!!!");
    }

    public void CurrentTarget()
    {        
        Enemy[] currentEnemies = FindObjectsOfType<Enemy>();

        float currentTargetDistance = Mathf.Infinity;
        foreach (var enemy in currentEnemies)
        {
            currentEnemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (currentEnemyDistance < currentTargetDistance)
            {
                currentTargetDistance = currentEnemyDistance;
                currentTarget = enemy.CurrentObject;
            }
        }
        currentTargetDistance = Mathf.Infinity;
        
        
    }

    public void RunToTarget()
    {
        CurrentTarget();
        MaximumAttackDistance();
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
        //Attack();
       // Die();
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        
        RunToTarget();
      


    }


    




}
