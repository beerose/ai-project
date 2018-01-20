using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public float health = 3;
    public float energy = 20;

    public float healthLimit = 5;
    public float healthDelay = 4;

    public float energyLimit = 5;
    public float energyDelay = 3;

    public GameObject DeathEffect;

    private float currentHealth;

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private float currentEnergy;

    public float CurrentEnergy
    {
        get { return currentEnergy; }
    }

    private float energyTimer = 0;
    private float healthTimer = 0;

    void Start()
    {
        currentHealth = health;
        currentEnergy = energy;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(DeathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        rechargeHealth();
        rechargeEnergy();
    }

    private void rechargeEnergy()
    {
        if (currentEnergy < energyLimit)
        {
            if (energyTimer <= 0)
            {
                currentEnergy++;
                energyTimer = energyDelay;
            }
        }
        energyTimer = updateTime(energyTimer);
    }

    private void rechargeHealth()
    {
        if (currentHealth < healthLimit)
        {
            if (healthTimer <= 0)
            {
                currentHealth++;
                healthTimer = healthDelay;
            }
        }
        healthTimer = updateTime(healthTimer);
    }

    private float updateTime(float timer)
    {
        if (timer > 0)
        {
            return timer - Time.deltaTime;
        }
        return timer;
    }

    void OnTriggerEnter(Collider coll)
    {
        string colTag = coll.transform.tag;
        if (colTag.Equals("Bullet"))
        {
            currentHealth -= coll.gameObject.GetComponent<ShotMover>().Power;
        }
    }

    public void useEnergy(float value)
    {
        currentEnergy -= value;
    }

    public bool isEnergy(float value)
    {
        return value <= currentEnergy;
    }
}