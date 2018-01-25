using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
	public float attackCost;
	public float incentive;
	public float attackDistance;

	public abstract bool isAvailable (Transform playerTransform);
	public abstract double getRating (Collider player, float enemyEnergy, float distance);
	public abstract double getRating (float enemyEnergy);
	public abstract void attack (Collider player);
    
}
