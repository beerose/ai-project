using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

	private Weapon[] weapons;
	private EnemyController enemyController;
	private SphereCollider sphereCollider;

	void Start(){
		enemyController = transform.parent.GetComponentInChildren<EnemyController>();
		weapons = transform.parent.GetComponentsInChildren <Weapon> ();
		sphereCollider = transform.GetComponent<SphereCollider> ();
	}
		

	void OnTriggerStay(Collider other){  
		if (other.tag.Equals("Player") && isAttackPossible (other)){
			attack (other);
		}
	}

	private bool isAttackPossible(Collider player){
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].isAvailable (player.transform) && enemyController.isEnergy (weapons[i].attackCost)) {
				return true;
			}
		}
		return false;
	}

	private void attack(Collider player){
		Weapon bestWeapon = null;
		double bestRating = double.NegativeInfinity;
		for (int i = 0; i < weapons.Length; i++) {
			double rating = weapons[i].getRating (player, enemyController.CurrentEnergy, sphereCollider.radius);
			if (rating > bestRating && weapons[i].isAvailable (player.transform) && enemyController.isEnergy (weapons[i].attackCost)) {
				bestWeapon = weapons[i];
				bestRating = rating;
			}
		}

		if (bestWeapon) {
			bestWeapon.attack (player);
			enemyController.useEnergy (bestWeapon.attackCost);
		}
	}

	public double getWeaponsRating(){
		double sum = 0;
		for (int i = 0; i < weapons.Length; i++) {
			double rating = weapons[i].getRating (enemyController.CurrentEnergy);
			if (enemyController.isEnergy (weapons[i].attackCost)) {
				sum += rating;
			}
		}
		return (weapons.Length == 0) ? 0 : sum / weapons.Length;
	}

	public double getBestWeaponRating(){
		double bestRating = double.NegativeInfinity;
		for (int i = 0; i < weapons.Length; i++) {
			double rating = weapons[i].getRating (enemyController.CurrentEnergy);
			if (rating > bestRating && enemyController.isEnergy (weapons[i].attackCost)) {
				bestRating = rating;
			}
		}
		return bestRating;
	}
}
