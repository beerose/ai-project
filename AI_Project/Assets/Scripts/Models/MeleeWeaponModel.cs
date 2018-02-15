using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Environment/MeleeWeapon")]
public class MeleeWeaponModel : WeaponModel  {
	
	public override double getRating(float enemyEnergy){
		double playerHealth = 100;
		double second = 60;
		double rDelay =  second / attackDelay;
		double rEnergy = enemyEnergy / attackCost;
		double rDemage = attackDemage * System.Math.Min (rDelay, rEnergy);
		return (rDemage > playerHealth) ? 100.0 : (rDemage/playerHealth) * 100.0 ;
	}

	public override GameObject create(){
		GameObject gameObject = Instantiate(weaponPrefab);
		MeleeWeapon meleeWeapon = gameObject.GetComponent<MeleeWeapon> ();

		meleeWeapon.attackDelay = attackDelay;
		meleeWeapon.attackDemage = attackDemage;
		meleeWeapon.attackCost = attackCost;
		meleeWeapon.attackDistance = attackDistance;
		meleeWeapon.incentive = incentive;

		return gameObject;
	}

}
