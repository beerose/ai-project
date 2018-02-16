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

	private EnemyModel[] population;
	private List<EnemyModel> newPopulation;


	public void start(EnemyModel enemy){
		population = new EnemyModel[numberOfPopulation];
		newPopulation = new List<EnemyModel>();
		initPopulation (enemy);
		sortAndMoveToPopulation ();
		for (int i = 0; i < numberIteration; i++) {
			crossEnemies ();
			foreach (EnemyModel e in population) {
				newPopulation.Add (e);
			}
			sortAndMoveToPopulation ();
			mutateEnemies ();
			foreach (EnemyModel e in population) {
				newPopulation.Add (e);
			}
			sortAndMoveToPopulation ();
		}
	}

	private void initPopulation(EnemyModel enemy){
		for (int i = 0; i < population.Length; i++){
			newPopulation.Add( initEnemy (enemy));
		}
	}

	private EnemyModel initEnemy (EnemyModel enemy){
		EnemyModel newEnemy = Instantiate (enemy);

		for (int i = 0; i < enemy.weaponModels.Length; i++) {
			WeaponModel newWeaponModel = Instantiate (enemy.weaponModels[i]);
			if (newWeaponModel.GetType().Equals(typeof(MeleeWeaponModel))) {
				newWeaponModel.attackDemage = randomValue (1, 10);
			} else {
				newWeaponModel.attackDemage = randomValue (2, 20);
			}
			newEnemy.weaponModels[i] = newWeaponModel;
		}

		newEnemy.runningSpeed = randomValue(2,15);
		newEnemy.walkSpeed = randomValue(2, (int) newEnemy.runningSpeed);
		newEnemy.energy = randomValue(10,30);
		newEnemy.health = randomValue(2,8); 

		return newEnemy;
	}
		
	private void sortAndMoveToPopulation(){
		newPopulation.Sort(SortByScore);

		int j = 0;
		foreach (EnemyModel enemy in newPopulation) {
			if (j >= population.Length) {
				break;
			} 
			population[j] = enemy;
			j++;
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

	private EnemyModel crossEnemy(EnemyModel enemy1, EnemyModel enemy2){
		EnemyModel newEnemy = Instantiate(enemy2);

		for (int i = 0; i < enemy1.weaponModels.Length || i < newEnemy.weaponModels.Length; i = i + 2) {
			newEnemy.weaponModels [i].attackDemage = enemy1.weaponModels [i].attackDemage;
		}
			
		newEnemy.runningSpeed = enemy1.runningSpeed;
		newEnemy.health = enemy1.health;

		return newEnemy;
	}

	private void mutateEnemies(){
		for(int i = 0; i < numberOfPopulation; i++){
			List<EnemyModel> mutantEnemiesPopulation = new List<EnemyModel>();
			for (int j = 0; j < numberOfMutantEnemies; j++) {
				mutantEnemiesPopulation.Add( mutateEnemy(population[i]));
			}

			mutantEnemiesPopulation.Sort(SortByScore);
			newPopulation.Add (mutantEnemiesPopulation[0]);
		}
	}

	private EnemyModel mutateEnemy (EnemyModel enemy){
		EnemyModel newEnemy = Instantiate (enemy);

		for (int i = 0;  i< enemy.weaponModels.Length; i++) {
			WeaponModel newWeaponModel = Instantiate (newEnemy.weaponModels [i]);
			newWeaponModel.attackDemage = mutate (newWeaponModel.attackDemage);
			newEnemy.weaponModels [i] = newWeaponModel;
		}

		newEnemy.runningSpeed = mutate (newEnemy.runningSpeed);
		newEnemy.energy = mutate (newEnemy.energy);
		newEnemy.health = mutate (newEnemy.health);

		return newEnemy;
	}

	private float mutate(float value){
		float newValue = value + randomValue(-1,1); 
		if(newValue <= 0 ){
			return value;
		}
		return newValue;
	}

	private int SortByScore(EnemyModel e1, EnemyModel e2){
		double resultE1 = System.Math.Abs(e1.getRating () - getScore ());
		double resultE2 = System.Math.Abs(e2.getRating () - getScore ());
		return (int) resultE1.CompareTo(resultE2);
	}

	private double getScore(){
		return 10 * level;
	}

	private int randomValue(int from, int to){
		Random.InitState(seed++);
		return Random.Range (from, to);
	}

	public EnemyModel[] getPopulation(){
		return population;
	}
		
}
