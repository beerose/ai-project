using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
	private const int numberOfPopulation = 15;
	private const int numberIteration = 10;
	private const int bestEnemies = 10;
	private const int numberOfMutantEnemies = 5;
	private const int numberNewPopulation = 10;
	public int level = 1;

	private GameObject[] population;
	private List<GameObject> newPopulation;


	public void start(GameObject enemy){
		population = new GameObject[numberOfPopulation];
		newPopulation = new List<GameObject>();
		initPopulation (enemy);
		sortPopulation ();
		for (int i = 0; i < numberIteration; i++) {
			crossEnemies ();
			foreach (GameObject e in population) {
				newPopulation.Add (e);
			}
			sortPopulation ();
			mutateEnemies ();
			foreach (GameObject e in population) {
				newPopulation.Add (e);
			}
			sortPopulation ();
		}
	}

	private void initPopulation(GameObject enemy){
		for (int i = 0; i < population.Length; i++){
			newPopulation.Add( initEnemy (Instantiate(enemy)));
		}
	}


	private GameObject initEnemy (GameObject gameObject){
		EnemyController enemyController = gameObject.GetComponentInChildren<EnemyController> ();
		EnemyMovement enemyMovement = gameObject.GetComponentInChildren<EnemyMovement> ();

		WeaponController [] weaponControllers = gameObject.GetComponentsInChildren<WeaponController> ();
		MeleeWeapon[] meleeWeapons = gameObject.GetComponentsInChildren<MeleeWeapon> ();

		for (int i = 0; weaponControllers.Length > i; i++) {
			weaponControllers[i].Damage = Random.Range(1, 20);
			weaponControllers[i].BulletSpeed = Random.Range(5, 15);
			weaponControllers [i].FireDelay = Random.Range (0, 10);
		}
			
		for (int i = 0; meleeWeapons.Length > i; i++) {
			meleeWeapons [i].attackDemage = Random.Range (1, 10);
		}

		enemyMovement.runningSpeed = Random.Range(1, 15);
		enemyMovement.walkSpeed = Random.Range(1, enemyMovement.runningSpeed);

		enemyController.energy = Random.Range(10, 30);
		enemyController.energyLimit = Random.Range(1, enemyController.energy);

		enemyController.health = Random.Range(1, 10);
		enemyController.healthLimit = Random.Range(1, enemyController.health);

		return gameObject;
	}

	private void sortPopulation(){
		newPopulation.Sort(SortByScore);

		int j = 0;
		foreach (GameObject enemy in newPopulation) {
			if (j >= population.Length) {
				Destroy(enemy);
			} else {
				population[j] = enemy;
				j++;
			}
		}

		newPopulation.Clear(); 
	}

	private void crossEnemies (){
		for(int i = 0; i < numberNewPopulation; i++){
			Random.InitState(i);
			int id1 = Random.Range(0, bestEnemies);
			Random.InitState(id1);
			int id2 = Random.Range (0, bestEnemies);
			newPopulation.Add( crossEnemy (population[id1], population[id2] ));
		}
	}

	private GameObject crossEnemy(GameObject gameObject1, GameObject gameObject2){
		GameObject gameObject = Instantiate(gameObject2);

		EnemyController enemyController = gameObject.GetComponentInChildren<EnemyController> ();
		EnemyController enemyController1 = gameObject1.GetComponentInChildren<EnemyController> ();

		EnemyMovement enemyMovement = gameObject.GetComponentInChildren<EnemyMovement> ();
		EnemyMovement enemyMovement1 = gameObject1.GetComponentInChildren<EnemyMovement> ();

		WeaponController [] weaponControllers = gameObject.GetComponentsInChildren<WeaponController> ();
		WeaponController [] weaponControllers1 = gameObject1.GetComponentsInChildren<WeaponController> ();

		for (int i = 0; weaponControllers.Length > i || weaponControllers1.Length > i; i++) {
			weaponControllers [i].Damage = weaponControllers1 [i].Damage;
			weaponControllers [i].FireDelay = weaponControllers1 [i].FireDelay;
			weaponControllers [i].BulletSpeed = weaponControllers1 [i].BulletSpeed;
		}
			
		enemyMovement.runningSpeed = enemyMovement1.runningSpeed;
		enemyMovement.runningCost = enemyMovement.runningCost;
		enemyMovement.walkSpeed = enemyMovement1.walkSpeed;

		enemyController.health = enemyController1.health;
		enemyController.healthDelay = enemyController1.healthDelay;
		enemyController.healthLimit = enemyController1.healthLimit;

		return gameObject;
	}

	private void mutateEnemies(){
		for(int i = 0; i < bestEnemies; i++){
			List<GameObject> mutantEnemiesPopulation = new List<GameObject>();
			for (int j = 0; j < numberOfMutantEnemies; j++) {
				mutantEnemiesPopulation.Add( mutateEnemy(population[i]));
			}

			mutantEnemiesPopulation.Sort(SortByScore);
			newPopulation.Add (mutantEnemiesPopulation[0]);

			int k = 0;
			foreach (GameObject enemy in mutantEnemiesPopulation) {
				if (k > 0) {
					Destroy (enemy);
				}
				k++;
			}
		}
	}

	private GameObject mutateEnemy (GameObject gameObject){
		GameObject result = Instantiate (gameObject);
		EnemyController enemyController = result.GetComponentInChildren<EnemyController> ();
		EnemyMovement enemyMovement = result.GetComponentInChildren<EnemyMovement> ();

		WeaponController [] weaponControllers = result.GetComponentsInChildren<WeaponController> ();
		MeleeWeapon[] meleeWeapons = result.GetComponentsInChildren<MeleeWeapon> ();

		for (int i = 0;  i< weaponControllers.Length; i++) {
			weaponControllers [i].Damage = mutate (weaponControllers [i].Damage);
		}

		for (int i = 0; i < meleeWeapons.Length; i++) {
			meleeWeapons [i].attackDemage = mutate (meleeWeapons [i].attackDemage);
		}

		enemyMovement.runningSpeed = mutate (enemyMovement.runningSpeed);

		enemyController.energy = mutate (enemyController.energy);

		enemyController.health = mutate (enemyController.health);

		return result;
	}

	private float mutate(float value){
		float newValue = value + Random.Range (-1, 1);
		if(newValue <= 0 ){
			return value;
		}
		return newValue;
	}

	private int SortByScore(GameObject e1, GameObject e2){
		EnemyController enemyController1 = e1.GetComponentInChildren<EnemyController> ();
		EnemyController enemyController2 = e2.GetComponentInChildren<EnemyController> ();
		double resultE1 = System.Math.Abs(enemyController1.getRating () - getScore ());
		double resultE2 = System.Math.Abs(enemyController2.getRating () - getScore ());
		return (int) resultE1.CompareTo(resultE2);
	}

	private double getScore(){
		return 10 * level;
	}

	public GameObject[] getPopulation(){
		return population;
	}
		
}
