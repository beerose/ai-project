using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : Weapon {
	public float attacksNumber;
	private WeaponController weaponController;

	void Start(){
		weaponController = GetComponent<WeaponController> ();
	}

	public override bool isAvailable (Transform playerTransform) {
		if(weaponController.isAvailable() && attacksNumber > 0){
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit, attackDistance)) {
				if (hit.collider.gameObject.tag == "Player") {
					return true;
				}
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
		weaponController.Fire(attackDemage);
	}

}
