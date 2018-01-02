using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

	public GameObject[] weapons = new GameObject[10];
	private EnemyController enemyController;

	void Start(){
		enemyController = gameObject.GetComponent<EnemyController>();
	}

	public bool isAttackPossible(Collider other){
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons [i]) {
				Weapon weapon = weapons [i].GetComponent<Weapon> ();
				if (weapon.isAvailable (other,transform) && enemyController.isEnergy (weapon.attackCost)) {
					return true;
				}
			}
		}
		return false;
	}

	public void attack(Collider other){
		Weapon bestWeapon = null;
		float bestRating = float.NegativeInfinity;
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons [i]) {
				Weapon weapon = weapons [i].GetComponent<Weapon> ();
				float rating = weapon.getRating (other, enemyController);
				if (rating > bestRating && weapon.isAvailable (other,transform) && enemyController.isEnergy (weapon.attackCost)) {
					bestWeapon = weapon;
					bestRating = rating;
				}
			}
		}
		bestWeapon.attack (other);
		enemyController.useEnergy (bestWeapon.attackCost);
	}
}
