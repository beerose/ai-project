using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
	public float attackDemage;
	public float attackDistance;
	public float attackCost;
	public float attackDelay;
	public float incentive;
	protected float attackTimer;

	public abstract bool isAvailable (Transform playerTransform);
	public abstract float getRating (Collider player, EnemyController enemyController);
	public abstract void attack (Collider player);

	void Update(){
		attackTimer = updateTime (attackTimer);
	}

	private float updateTime(float timer){
		if (timer > 0) {
			return timer - Time.deltaTime;
		}
		return timer;
	}
}
