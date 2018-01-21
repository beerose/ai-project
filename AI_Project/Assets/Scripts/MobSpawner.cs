using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawner : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
        Invoke("Spawn", 0.5f);
        Debug.Log(GetComponent<NavMeshAgent>().agentTypeID);
    }

    private void Spawn()
    {
        gameObject.SetActive(true);
    }
}