using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

	private Weapon[] weapons;
	private EnemyController enemyController;

	void Start(){
		enemyController = gameObject.GetComponent<EnemyController>();
		weapons = gameObject.GetComponentsInChildren<Weapon> ();
	}

	public bool isAttackPossible(Collider player){
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].isAvailable (player.transform) && enemyController.isEnergy (weapons[i].attackCost)) {
				return true;
			}
		}
		return false;
	}

	public void attack(Collider player){
		Weapon bestWeapon = null;
		float bestRating = float.NegativeInfinity;
		for (int i = 0; i < weapons.Length; i++) {
			float rating = weapons[i].getRating (player, enemyController);
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
}
