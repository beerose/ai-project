using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject PickUPEffect;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().AddPotion();
            Instantiate(PickUPEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
