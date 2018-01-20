using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Weapon {
	public float attacksNumber;
	private WeaponController weapon;

	void Start(){
		weapon = GetComponent<WeaponController> ();
		InvokeRepeating("Fire", 0f, weapon.fireRate);
	}

	public override bool isAvailable (Transform playerTransform) {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, attackDistance)) {
			if (hit.collider.gameObject.tag == "Player" && attackTimer <= 0 && attacksNumber > 0) {
				return true;
			}
		}
		return false;
	}

	public override float getRating (Collider player, EnemyController enemyController){
		PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth> (); 
		Vector3 otherPosition = player.transform.position;
		float distance = Vector3.Distance(transform.position, otherPosition);
		float energy = 100 - (attackCost / enemyController.CurrentEnergy * 100);
		float demage = attackDemage / playerHealth.CurrentHealth * 100;
		return - distance + (energy + demage) * incentive ;
	}

	public override void attack(Collider player){
		weapon.Fire();
		attackTimer = attackDelay;
	}

}
