using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Environment/Enemy")]
public class EnemyModel : ScriptableObject {
	public GameObject enemyPrefab;

	public float walkSpeed;

	public float runningSpeed;
	public float runningCost;
	public float runningDelay;

	public bool playerTracking;

	public float health;
	public float energy;

	public float healthLimit;
	public float healthDelay;

	public float energyLimit;
	public float energyDelay;

	public WeaponModel[] weaponModels;

	public double getRating(){
		double rShooting = getWeaponsRating();
		double rMovement = getRatingMovement();

		double rHealth = System.Math.Min(100, health * 3);

		double rEnergy = System.Math.Min(100, energy);

		return System.Math.Min(100, ((rEnergy + rHealth + rMovement)/2 + rShooting) / 400 * 100);
	}

	private double getRatingMovement(){
		double rEnergy = energy / runningCost;
		double rDelay =  60 / runningDelay;
		double rSpeed = runningSpeed * System.Math.Min (rDelay, rEnergy);
		double rWalk = (600 / walkSpeed) / 600 * 100;
		double rPlayerTracking = (playerTracking) ? 100 : 0;

		double resultSpeed = (rSpeed > 600) ? 100 : ((rSpeed / 600) * 100);
		return  (resultSpeed + rWalk + rPlayerTracking > 200) ? 100 : (resultSpeed + rWalk + rPlayerTracking)/300 * 100;
	}

	public double getWeaponsRating(){
		double sum = 0;
		for (int i = 0; i < weaponModels.Length; i++)
		{
			double rating = weaponModels[i].getRating(energy);
			if (energy > weaponModels[i].attackCost)
			{
				sum += rating;
			}
		}
		return (weaponModels.Length == 0) ? 0.0 : sum / weaponModels.Length;
	}

	public GameObject createEnemy(){
		GameObject gameObject = Instantiate (enemyPrefab);
		EnemyController enemyController = gameObject.GetComponentInChildren<EnemyController> ();
		EnemyMovement enemyMovement = gameObject.GetComponentInChildren<EnemyMovement> ();
		EnemyShooting enemyShooting = gameObject.GetComponentInChildren<EnemyShooting> ();

		enemyController.energy = energy;
		enemyController.energyDelay = energyDelay;
		enemyController.energyLimit = energyLimit;

		enemyController.health = health;
		enemyController.healthDelay = healthDelay;
		enemyController.healthLimit = healthLimit;

		enemyMovement.playerTracking = playerTracking;
		enemyMovement.runningCost = runningCost;
		enemyMovement.runningDelay = runningDelay;
		enemyMovement.runningSpeed = runningSpeed;
		enemyMovement.walkSpeed = walkSpeed;

		for (int i = 0; i < weaponModels.Length; i++) {
			GameObject weapon = weaponModels [i].create ();
			weapon.transform.SetParent (gameObject.transform.Find("Brain"));
			weapon.transform.position = new Vector3 (0.0f,0f,0.6f);
		}
			
		return gameObject;
	}


}