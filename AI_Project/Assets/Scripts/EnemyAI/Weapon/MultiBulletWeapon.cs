using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBulletWeapon : Weapon {
	public float attacksNumber;
	private WeaponController[] weaponControllers;
	private float diff;
	private Quaternion startRotation;

	void Start(){
		weaponControllers = GetComponents<WeaponController> ();
		startRotation = transform.rotation;
		diff = 90 / weaponControllers.Length;
	}

	public override bool isAvailable (Transform playerTransform) {

		if (weaponControllers.Length == 0 || attacksNumber == 0)
			return false;

		for (int i = 0; i < weaponControllers.Length; i++) {
			if (!weaponControllers [i].isAvailable ())
				return false;
		}
		transform.rotation = startRotation;	
		transform.Rotate (Vector3.up, -45);
		for (int i = 0; i < weaponControllers.Length; i++) {
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
		double rDemage = averagePower() / playerHealth.m_StartingHealth * 100;
		double rSpeed = averageSpeed();
		return - (rDistance / rSpeed) + weaponControllers.Length + (rEnergy + rDemage) * incentive ;
	}

	public override double getRating(float enemyEnergy){
		PlayerHealth playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth>(); 
		double rEnergy = 100 - (attackCost / enemyEnergy * 100);
		double rDemage = averagePower() / playerHealth.m_StartingHealth * 100;
		double rSpeed = averageSpeed() / 20 * 100;
		double rDelay = maxDelay() / 10 * 100;
		return - rDelay - (100 / rSpeed) + weaponControllers.Length + (rEnergy + rDemage) * incentive ;
	}

	private double averagePower(){
		double average = 0;
		for (int i = 0; i < weaponControllers.Length; i++) {
			average += weaponControllers [i].Damage;
		}
		return average / weaponControllers.Length;
	}

	private double averageSpeed(){
		double average = 0;
		for (int i = 0; i < weaponControllers.Length; i++) {
			average += weaponControllers [i].BulletSpeed;
		}
		return average / weaponControllers.Length;
	}

	private double maxDelay(){
		double max = 0;
		for (int i = 0; i < weaponControllers.Length; i++) {
			if (max < weaponControllers [i].FireDelay)
				max = weaponControllers [i].FireDelay;
		}
		return max;
	}

	public override void attack(Collider player){
		transform.rotation = startRotation;
		transform.Rotate (Vector3.up, -45);
		for (int i = 0; i < weaponControllers.Length; i++) {
			transform.Rotate (Vector3.up, diff);
			weaponControllers [i].Fire ();
		}
	}
		
}
