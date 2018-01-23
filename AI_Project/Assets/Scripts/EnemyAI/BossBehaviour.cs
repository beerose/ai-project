using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public GameObject Spawn;
    public float minimumScale = 0.5f;
    public int Phase1SpawnNumber = 2;
    public int Phase2SpawnNumber = 4;
    public int Phase3SpawnNumber = 8;
    private Vector3 startScale;
    private Vector3 currentScale;
    private float startHP;
    private float currentHP;
    private int phase;

    private void Start()
    {
        startScale = transform.localScale;
        startHP = GetComponentInChildren<EnemyController>().health;
    }

    private void LateUpdate()
    {
        float tmp = currentHP;
        currentHP = GetComponentInChildren<EnemyController>().CurrentHealth;
        if (tmp > currentHP) setSize();
    }

    private void setSize()
    {
        var ratio = currentHP / startHP;
        currentScale = ratio * startScale;
        if (currentScale.x <= minimumScale) currentScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.localScale = currentScale;

        if (ratio <= 0.75f && ratio > 0.5f && phase == 0)
        {
            phase++;
            Spawner(Phase1SpawnNumber);
        }
        if (ratio <= 0.5f && ratio > 0.25f && phase == 1)
        {
            phase++;
            Spawner(Phase2SpawnNumber);
        }
        if (ratio <= 0.25f && ratio > 0f && phase == 2)
        {
            phase++;
            Spawner(Phase3SpawnNumber);
        }
    }

    private void Spawner(int n)
    {
        for (var i = 0; i < n; i++)
        {
            Instantiate(Spawn, transform.position, transform.rotation).GetComponentInChildren<EnemyController>()
                .SetAlwaysSpawn();
        }
    }
}