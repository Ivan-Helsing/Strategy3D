using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardHealthBar : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
