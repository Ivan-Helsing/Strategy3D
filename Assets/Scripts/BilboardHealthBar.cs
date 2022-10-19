using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardHealthBar : MonoBehaviour
{
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void ShowHealth()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
