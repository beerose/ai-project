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

    private bool alwaysSpawn = false;

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
            Destroy(transform.parent.gameObject);
        }
        rechargeHealth();
        rechargeEnergy();
		EnemyShooting enemyShooting = transform.parent.GetComponentInChildren<EnemyShooting> ();
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
            string shooter = coll.GetComponent<ShotMover>().GetShooterTag();
            if (shooter.Equals("Player"))
            {
                currentHealth -= coll.gameObject.GetComponent<ShotMover>().Damage;
            }
        }
    }

	public double getRating(){
		EnemyShooting enemyShooting = transform.parent.GetComponentInChildren<EnemyShooting> ();
		EnemyMovement enemyMovement = transform.parent.GetComponentInChildren<EnemyMovement> ();
		SphereCollider sphereCollider = enemyMovement.GetComponent<SphereCollider> ();
		PlayerHealth playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth>(); 

		double rShooting = enemyShooting.getWeaponsRating () + enemyShooting.getBestWeaponRating ();
		double rMovement = enemyMovement.getRating () + sphereCollider.radius * 2;

		double rHealth = (health / playerHealth.m_StartingHealth * 100) ;
		double rHealthLimit = (healthLimit == 0 )? 0: (health / healthLimit * 100);
		double rHealthDelay = healthDelay / 10 * 100;

		double rEnergy = energy ;
		double rEnergyLimit = (energyLimit == 0 )? 0: (energy / energyLimit * 100);
		double rEnergyDelay = energyDelay / 10 * 100;

		return -rEnergyDelay - rHealthDelay + rEnergyLimit + rHealthLimit + rEnergy + rHealth + rMovement + rShooting;
	}

    public void useEnergy(float value)
    {
        currentEnergy -= value;
    }

    public bool isEnergy(float value)
    {
        return value <= currentEnergy;
    }

    public void SetAlwaysSpawn()
    {
        alwaysSpawn = true;
    }

    public bool GetAlwaysSpawn()
    {
        return alwaysSpawn;
    }
}