using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    public int HP;
    public GameObject DeathEffect;

    void Start()
    {
    }

    void Update()
    {
        

        if (HP <= 0)
        {
            Instantiate(DeathEffect, transform.position, transform.rotation);
            Destroy(gameObject, 0);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        string colTag = col.transform.tag;
        if (colTag == "Bullet")
        {
            HP -= 1;
        }
    }
}