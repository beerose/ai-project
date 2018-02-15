using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Environment/BulletWeapon")]
public class BulletWeaponModel : WeaponModel {
	public float attacksNumber;
	public float bulletSpeed;

	public override double getRating(float enemyEnergy){
		double playerHealth = 100;
		double second = 60;
		double rDelay =  second / attackDelay;
		double rEnergy = enemyEnergy / attackCost;
		double rDemage = attackDemage * System.Math.Min (rDelay, rEnergy);

		double rSpeed =  100.0 - (bulletSpeed / 20.0 * 100.0);

		return (rDemage >= 100) ? 200.0 : (rDemage/playerHealth) * 100.0 + (rSpeed) / 200.0 * 100.0;
	}

	public override GameObject create(){
		GameObject gameObject = Instantiate(weaponPrefab);
		WeaponController weaponController = gameObject.GetComponent<WeaponController> ();
		BulletWeapon bulletWeapon = gameObject.GetComponent<BulletWeapon> ();

		bulletWeapon.attacksNumber = attacksNumber;
		bulletWeapon.attackCost = attackCost;
		bulletWeapon.attackDistance = attackDistance;
		bulletWeapon.incentive = incentive;

		weaponController.BulletSpeed = bulletSpeed;
		weaponController.Damage = attackDemage;
		weaponController.FireDelay = attackDelay;

		return gameObject;
	}
}
