using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    private NavMeshSurface[] surfaces;

    void Start()
    {
        gameObject.SetActive(false);
        Invoke("active", 0.5f);
    }

    private void active()
    {
        gameObject.SetActive(true);
    }

    void OnEnable()
    {
        surfaces = FindObjectsOfType<NavMeshSurface>();
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

}