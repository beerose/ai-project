using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Environment/MultiBulletWeapon")]
public class MultiWeaponModel : WeaponModel {
	public int bulletsNumber;
	public float angle;
	public float attacksNumber;
	public float bulletSpeed;

	public override double getRating(float enemyEnergy){
		double playerHealth = 100;
		double second = 60;
		double rDelay = second / attackDelay;
		double rEnergy = enemyEnergy / attackCost;
		double rDemage = attackDemage * System.Math.Min (rDelay, rEnergy);
		double resultDemage =  (rDemage > playerHealth) ? 100.0 : (rDemage/playerHealth) * 100.0 ;

		double rSpeed =  100.0 - (bulletSpeed / 20.0 * 100.0);
		double rNumber = (double) bulletsNumber / 36.0 * 100;

		return  (rDemage >= 100) ? 200.0 : (rDemage/playerHealth) * 100.0 + (rSpeed + rNumber) / 200.0 * 100.0;
	}

	public override GameObject create(){
		GameObject gameObject = Instantiate(weaponPrefab);
		WeaponController weaponController = gameObject.GetComponent<WeaponController> ();
		MultiBulletWeapon multiBulletWeapon = gameObject.GetComponent<MultiBulletWeapon> ();

		multiBulletWeapon.attacksNumber = attacksNumber;
		multiBulletWeapon.attackCost = attackCost;
		multiBulletWeapon.attackDistance = attackDistance;
		multiBulletWeapon.incentive = incentive;
		multiBulletWeapon.angle = angle;
		multiBulletWeapon.bulletsNumber = bulletsNumber;

		weaponController.BulletSpeed = bulletSpeed;
		weaponController.Damage = attackDemage;
		weaponController.FireDelay = attackDelay;

		return gameObject;
	}
}
