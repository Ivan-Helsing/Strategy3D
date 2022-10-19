using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float destroyTime;

    public event Action OnThisEnemyDie;

    public GameObject CurrentEnemyObject { get => gameObject; }

    private void Start()
    {
        Invoke("SelfKilling", destroyTime);
    }

    void SelfKilling()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        OnThisEnemyDie?.Invoke();
    }


}
