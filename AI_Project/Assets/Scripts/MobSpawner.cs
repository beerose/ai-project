using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
        Invoke("Spawn", 1f);
    }

    private void Spawn()
    {
        gameObject.SetActive(true);
    }
}