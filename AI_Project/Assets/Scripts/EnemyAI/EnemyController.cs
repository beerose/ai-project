using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float health = 20;
	public float energy = 20;

	public float healthLimit = 5;
	public float healthDelay = 4;

	public float energyLimit = 5;
	public float energyDelay = 3;

	private float currentHealth;
	private float currentEnergy;
	private float energyTimer = 0;
	private float healthTimer = 0;

	void Start(){
		currentHealth = health;
		currentEnergy = energy;
	}
		
	void Update(){
		if (currentHealth <= 0){
			Destroy(gameObject, 0);
		}
		rechargeHealth ();
		rechargeEnergy ();
	}

	private void rechargeEnergy(){
		if (currentEnergy < energyLimit) {
			if (energyTimer <= 0) {
				currentEnergy++;
				energyTimer = energyDelay;
			}
		}
		energyTimer = updateTime (energyTimer);
	}

	private void rechargeHealth(){
		if (currentHealth < healthLimit) {
			if (healthTimer <= 0) {
				currentHealth++;
				healthTimer = healthDelay;
			}
		}
		healthTimer = updateTime (healthTimer);
	}

	private float updateTime(float timer){
		if (timer > 0) {
			return timer - Time.deltaTime;
		}
		return timer;
	}

	void OnTriggerEnter(Collider collider) {
		string colTag = collider.transform.tag;
		if ( colTag.Equals("Bullet") ) {
			currentHealth -= 1; 
		}
	}

	public void useEnergy(float value){
		currentEnergy -= value;
	}

	public bool isEnergy(float value){
		return value <= currentEnergy;
	}
}
