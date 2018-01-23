using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawner : MonoBehaviour
{
    private EnemiesCollector EC;

    void Start()
    {
        gameObject.SetActive(false);
        Invoke("Spawn", 0.5f);
        EC = GameObject.Find("EnemiesCollector").GetComponent<EnemiesCollector>();
        //Debug.Log(GetComponent<NavMeshAgent>().agentTypeID);
    }

    private void Spawn()
    {
        if (GetComponentInChildren<EnemyController>().GetAlwaysSpawn())
            gameObject.SetActive(true);
        else
        {
            gameObject.SetActive(true);
            var hitColliders = Physics.OverlapSphere(transform.position, 1);
            foreach (var tango in hitColliders)
            {
                if (tango.CompareTag("Floor"))
                {
                    EC.Add(gameObject, tango.transform.parent.name);
                }
            }
            gameObject.SetActive(false);
        }
    }
}