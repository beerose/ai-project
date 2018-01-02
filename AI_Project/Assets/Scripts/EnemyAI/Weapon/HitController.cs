using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : Weapon {
	//private float attackDistance = 1;
	//private float demage = 10;
	//private float attackDelay = 1;
	//private float attackCost = 0.2f;
	//private float attackTimer = 0;

	public override bool isAvailable(Collider other, Transform enemyTransform){
		float distance = Vector3.Distance(transform.position, other.transform.position);
		return distance <= attackDistance && attackTimer <= 0;
	}
		
	public override float getRating (Collider other, EnemyController enemyController){
		PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth> (); 
		float distance = Vector3.Distance(transform.position, other.transform.position);
		float energy = 100 - (attackCost / enemyController.CurrentEnergy * 100);
		float demage = attackDemage / enemyController.CurrentHealth * 100;
		return (energy + demage) * incentive ;
	}

	public override void attack(Collider other){
		Debug.Log ("Hit!");
		other.SendMessage ("TakeDamage", attackDemage);
		attackTimer = attackDelay;
	}
		
}
