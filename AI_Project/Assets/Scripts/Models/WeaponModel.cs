using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponModel : ScriptableObject  {
	public GameObject weaponPrefab;
	public float attackCost;
	public float incentive;
	public float attackDistance;
	public float attackDelay;
	public float attackDemage;

	public abstract double getRating (float enemyEnergy);
	public abstract GameObject create ();
}
