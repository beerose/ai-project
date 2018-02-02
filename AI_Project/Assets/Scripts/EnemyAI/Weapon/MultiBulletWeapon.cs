using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBulletWeapon : Weapon {
	public int numberBullets;
	public float angle;
	public float attacksNumber;
	private WeaponController weaponController;
	private float diff;
	private Quaternion startRotation;
	private float delay;

	void Start(){
		weaponController = GetComponent<WeaponController> ();
		transform.Rotate (Vector3.up, -(angle/2));
		startRotation = transform.rotation;
		diff = angle / numberBullets;
		delay = weaponController.FireDelay;
	}

	public override bool isAvailable (Transform playerTransform) {

		if (numberBullets == 0 || attacksNumber == 0)
			return false;

		if (!weaponController.isAvailable ()){
				return false;
		}

		transform.rotation = startRotation;	
		for (int i = 0; i < numberBullets; i++) {
			RaycastHit hit;
			transform.Rotate (Vector3.up, diff);
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
		double rSpeed = weaponController.BulletSpeed;;
		return - (rDistance / rSpeed) + numberBullets + (rEnergy + rDemage) * incentive ;
	}

	public override double getRating(float enemyEnergy){
		PlayerHealth playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth>(); 
		double rEnergy = 100 - (attackCost / enemyEnergy * 100);
		double rDemage = weaponController.Damage / playerHealth.m_StartingHealth * 100;
		double rSpeed = weaponController.BulletSpeed / 20 * 100;
		double rDelay = weaponController.FireDelay / 10 * 100;
		return - rDelay - (100 / rSpeed) + numberBullets+ (rEnergy + rDemage) * incentive ;
	}
		
	public override void attack(Collider player){
		transform.rotation = startRotation;
		weaponController.FireDelay = 0;
		for (int i = 0; i < numberBullets; i++) {
			if (i == numberBullets - 1) {
				weaponController.FireDelay = delay;
			}
			transform.Rotate (Vector3.up, diff);
			weaponController.Fire ();
		}
		attacksNumber--;
	}
		
}
