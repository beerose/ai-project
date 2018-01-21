using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {
	public float attackDelay;
	private float attackTimer;

	public override bool isAvailable(Transform playerTransform){
		float distance = Vector3.Distance(transform.position, playerTransform.position);
		return distance <= attackDistance && attackTimer <= 0;
	}
		
	public override float getRating (Collider player, EnemyController enemyController){
		PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth> (); 
		float energy = 100 - (attackCost / enemyController.CurrentEnergy * 100);
		float demage = attackDemage / playerHealth.CurrentHealth * 100;
		return (energy + demage) * incentive ;
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
