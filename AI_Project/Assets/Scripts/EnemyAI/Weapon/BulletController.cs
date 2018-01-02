using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Weapon {
	public GameObject bullet;
	public Transform shotSpawn;
	public float attackQuantity;

	public override bool isAvailable (Collider other, Transform enemyTransform) {
		RaycastHit hit;
		if (Physics.Raycast (enemyTransform.position, transform.forward, out hit, attackDistance)) {
			if (hit.collider.gameObject.tag == "Player" && attackTimer <= 0 && attackQuantity > 0) {
				return true;
			}
		}
		return false;
	}

	public override float getRating (Collider other, EnemyController enemyController){
		PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth> (); 
		float distance = Vector3.Distance(transform.position, other.transform.position);
		float energy = 100 - (attackCost / enemyController.CurrentEnergy * 100);
		float demage = attackDemage / enemyController.CurrentHealth * 100;
		return - distance + (energy + demage) * incentive ;
	}

	public override void attack(Collider other){
		Debug.Log ("Bullet!");
		Instantiate(bullet, shotSpawn.position, shotSpawn.rotation);
		attackTimer = attackDelay;
	}

}
