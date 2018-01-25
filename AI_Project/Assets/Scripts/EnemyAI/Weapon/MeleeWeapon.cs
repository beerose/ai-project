using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {
	public float attackDelay;
	public float attackDemage;
	private float attackTimer;

	public override bool isAvailable(Transform playerTransform){
		float distance = Vector3.Distance(transform.position, playerTransform.position);
		return distance <= attackDistance && attackTimer <= 0;
	}
		
	public override double getRating (Collider player, float enemyEnergy, float distance){
		PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth> (); 
		double rEnergy = 100 - (attackCost / enemyEnergy * 100);
		double rDemage = attackDemage / playerHealth.CurrentHealth * 100;
		return (rEnergy + rDemage) * incentive ;
	}

	public override double getRating(float enemyEnergy){
		PlayerHealth playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth>(); 
		double rEnergy = 100 - (attackCost / enemyEnergy * 100);
		double rDemage = attackDemage / playerHealth.m_StartingHealth * 100;
		double rDelay = attackDelay / 10 * 100;
		return - rDelay + (rEnergy + rDemage) * incentive ;
	}

	public override void attack(Collider player){
		player.SendMessage ("TakeDamage", attackDemage);
		attackTimer = attackDelay;
	}

	void Update(){
		attackTimer = updateTime (attackTimer);
	}

	private float updateTime(float timer){
		if (timer > 0) {
			return timer - Time.deltaTime;
		}
		return timer;
	}
		
}
