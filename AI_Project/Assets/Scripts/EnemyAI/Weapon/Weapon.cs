using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
	public float attackDemage;
	public float attackDistance;
	public float attackCost;
	public float incentive;

	public abstract bool isAvailable (Transform playerTransform);
	public abstract float getRating (Collider player, EnemyController enemyController);
	public abstract void attack (Collider player);
    
}
