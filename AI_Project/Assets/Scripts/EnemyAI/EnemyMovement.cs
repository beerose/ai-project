using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
	public float walkSpeed = 2;
	public float radius = 3;
	public float runningSpeed = 8;
	public float runningCost = 0.1f;
	private float runningTimer = 0;
	private float runningDelay = 1;

	private EnemyController enemyController;
	private Vector3 startPosition;
	private NavMeshAgent agent;

	private float attackDistance = 1;
	private float attackDemage = 10;
	private float attackDelay = 1;
	private float attackCost = 0.2f;
	private float attackTimer = 0;

	void Start(){
		enemyController = gameObject.GetComponentInParent<EnemyController>();
		startPosition =  enemyController.transform.position; 
		agent = enemyController.GetComponent<NavMeshAgent> ();
	}

	void Update(){
		searchPlayer ();
	}
		

	private void searchPlayer(){
		agent.speed = walkSpeed;
		Vector3 randomPosition = randomNavSphere (startPosition, radius, -1);
		agent.SetDestination (randomPosition);
	}

	private Vector3 randomNavSphere (Vector3 origin, float distance, int layermask) {
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
		randomDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition (randomDirection, out navHit, distance, layermask);
		return navHit.position;
	}

	void OnTriggerStay(Collider other){
		if (other.tag.Equals ("Player")) {
			startPosition = other.transform.position;
			setRotation (other.transform);
			goAndHit (other);
		}
	}

	private void goAndHit(Collider other){
		float distance = Vector3.Distance(transform.position, other.transform.position);
		if (distance > attackDistance && enemyController.isEnergy (runningCost)) {
			if (runningTimer <= 0) {
				runningTimer = runningDelay;
				enemyController.useEnergy (runningCost);
			}
			agent.speed = runningSpeed;
			agent.SetDestination (other.transform.position);
		} else if (distance <= attackDistance && attackTimer <= 0 && enemyController.isEnergy (attackCost)) {
			other.SendMessage ("TakeDamage", attackDemage);
			attackTimer = attackDelay;
			enemyController.useEnergy (attackCost);
		}

		attackTimer = updateTime (attackTimer);
		runningTimer = updateTime (runningTimer);
	}

	private float updateTime(float timer){
		if (timer > 0) {
			return timer - Time.deltaTime;
		}
		return timer;
	}

	private void setRotation(Transform playerTransform){
		Transform enemyTransform = enemyController.transform; 
		Quaternion targetRotation = Quaternion.LookRotation (playerTransform.position - enemyTransform.position);
		float oryginalX = enemyTransform.rotation.x;
		float oryginalZ = enemyTransform.rotation.z;

		Quaternion finalRotation = Quaternion.Slerp (enemyTransform.rotation, targetRotation, 5.0f * Time.deltaTime);
		finalRotation.x = oryginalX;
		finalRotation.z = oryginalZ;
		enemyTransform.rotation = finalRotation;
	}
}
