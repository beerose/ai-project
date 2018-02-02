using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnRange : MonoBehaviour
{
    public GameObject Target;
    public float Range = 50;

    void Start()
    {
        
        InvokeRepeating("check", 2f, 1f);
    }

    void check()
    {
        if (Target==null)Target = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(transform.position, Target.transform.position) > Range) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}