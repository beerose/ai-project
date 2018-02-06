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

	public override double getRating (Collider player, float enemyEnergy, float distance){
		PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth> (); 
		Vector3 otherPosition = player.transform.position;
		double maxDistance = (attackDistance > distance) ? distance : attackDistance;
		double rDistance = Vector3.Distance(transform.position, otherPosition) / maxDistance * 100;
		double rEnergy = 100 - (attackCost / enemyEnergy * 100);
		double rDemage = weaponController.Damage / playerHealth.m_StartingHealth * 100;
		double rSpeed = weaponController.BulletSpeed;
		return - (rDistance / rSpeed) + (rEnergy + rDemage) * incentive ;
	}

	public override double getRating(float enemyEnergy){
		Start ();
		double playerHealth = 100;
		double second = 60;
		double rDelay =  second / weaponController.FireDelay;
		double rEnergy = enemyEnergy / attackCost;
		double rDemage = weaponController.Damage * System.Math.Min (rDelay, rEnergy);

		double rSpeed =  100.0 - (weaponController.BulletSpeed / 20.0 * 100.0);

		return (rDemage >= 100) ? 200.0 : (rDemage/playerHealth) * 100.0 + (rSpeed) / 200.0 * 100.0;
	}

	public override void attack(Collider player){
		weaponController.Fire();
	}

}
