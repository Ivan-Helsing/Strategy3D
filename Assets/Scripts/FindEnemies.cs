using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemies : MonoBehaviour
{
    private Enemy[] currentEnemies;

    void Start()
    {
        currentEnemies = FindObjectsOfType<Enemy>();
    }

    public Enemy[] CurrentEnemies() { return currentEnemies; }
}
