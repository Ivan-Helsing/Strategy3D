using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindEnemies : MonoBehaviour
{
    private Enemy[] currentEnemies;
    Enemy currentenemy;

    void Awake()
    {
        FindEnemy();

    }
    private void OnEnable()
    {
        foreach (var enemy in currentEnemies)
        {
            enemy.OnThisEnemyDie += FindEnemy;
            currentenemy = enemy;
        }
    }

    private void FindEnemy()
    {
        if (currentEnemies != null)
        {
            currentenemy.OnThisEnemyDie -= FindEnemy;
        }
        currentEnemies = FindObjectsOfType<Enemy>();
    }

    public Enemy[] CurrentEnemies() { return currentEnemies; }
}
