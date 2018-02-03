using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    private NavMeshSurface[] surfaces;

    void Start()
    {
        Invoke("bake", 0.5f);
    }

    private void bake()
    {
        surfaces = FindObjectsOfType<NavMeshSurface>();
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
        LoadingBar.Instance.Progress += 1;
    }

}