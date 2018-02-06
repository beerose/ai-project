using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
	private const int numberOfPopulation = 30;
	private const int numberIteration = 10;
	private const int numberOfMutantEnemies = 5;
	private const int numberNewPopulation = 5;
	public float level = 1;

	private int seed = 0;

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
			weaponControllers[i].Damage = randomValue(2,20);
			weaponControllers[i].BulletSpeed =  randomValue(5,15);
			weaponControllers [i].FireDelay = randomValue(0,10);
		}
			
		for (int i = 0; meleeWeapons.Length > i; i++) {
			meleeWeapons [i].attackDemage = randomValue(1,10);
		}
			
		enemyMovement.runningSpeed = randomValue(2,15);

		enemyMovement.walkSpeed = randomValue(2, (int)enemyMovement.runningSpeed);

		enemyController.energy = randomValue(10,30);

		enemyController.health = randomValue(2,8); 

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
			int id1 = randomValue(1,numberOfPopulation); 
			int id2 = randomValue(1,numberOfPopulation); 
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
		}
			
		enemyMovement.runningSpeed = enemyMovement1.runningSpeed;

		enemyController.health = enemyController1.health;

		return gameObject;
	}

	private void mutateEnemies(){
		for(int i = 0; i < numberOfPopulation; i++){
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
			Random.InitState(i);
			weaponControllers [i].Damage = mutate (weaponControllers [i].Damage);
		}

		for (int i = 0; i < meleeWeapons.Length; i++) {
			Random.InitState(i);
			meleeWeapons [i].attackDemage = mutate (meleeWeapons [i].attackDemage);
		}


		enemyMovement.runningSpeed = mutate (enemyMovement.runningSpeed);

		enemyController.energy = mutate (enemyController.energy);

		enemyController.health = mutate (enemyController.health);

		return result;
	}

	private float mutate(float value){
		float newValue = value + randomValue(-1,1); 
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

	private int randomValue(int from, int to){
		Random.InitState(seed++);
		return Random.Range (from, to);
	}

	public GameObject[] getPopulation(){
		return population;
	}
		
}
