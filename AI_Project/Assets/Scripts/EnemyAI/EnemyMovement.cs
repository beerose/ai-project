using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float walkSpeed = 2;
    public float radius = 3;
    public float runningSpeed = 8;
    public float runningCost = 0.1f;
    private float runningTimer = 0;
    private float runningDelay = 1;

    private EnemyController enemyController;
    private EnemyShooting enemyShooting;
    private Vector3 startPosition;
    private NavMeshAgent agent;
    private Animator anim;
	private Transform enemyTransform;

    void Start()
    {
		enemyTransform = transform.parent.transform;
        enemyController = transform.parent.GetComponentInChildren<EnemyController>();
        enemyShooting = transform.parent.GetComponentInChildren<EnemyShooting>();
		startPosition = enemyTransform.position;
        agent = transform.parent.GetComponent<NavMeshAgent>();
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        searchPlayer();
		runningTimer = updateTime(runningTimer);
    }

    private void searchPlayer()
    {
        agent.speed = walkSpeed;
        Vector3 randomPosition = randomNavSphere(startPosition, radius, -1);
        agent.SetDestination(randomPosition);
    }

    private Vector3 randomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet"))
        {
			Vector3 randomPosition = randomNavSphere(enemyTransform.position, radius, -1);
			if (enemyController.isEnergy (runningCost)) {
				run (randomPosition);
			} else {
				agent.speed = walkSpeed;
				agent.SetDestination(randomPosition);
			}
            startPosition = other.transform.position;
        }
    }

    void OnTriggerStay(Collider other)
    {  
        if (other.tag.Equals("Player"))
        {
            setRotation(other.transform);
            anim.SetTrigger("Attack");
            startPosition = other.transform.position;
            move(other);
        }
    }

    private void move(Collider other)
    {
		if (enemyShooting && enemyShooting.isAttackPossible (other)) {
			enemyShooting.attack (other);
		} else if (enemyController.isEnergy (runningCost)){
			run (other.transform.position);
		}
    }

	private void run(Vector3 targetPostion)
    {
        if (runningTimer <= 0)
        {
            runningTimer = runningDelay;
            enemyController.useEnergy(runningCost);
        }
        agent.speed = runningSpeed;
		agent.SetDestination(targetPostion);
    }

    private float updateTime(float timer)
    {
        if (timer > 0)
        {
            return timer - Time.deltaTime;
        }
        return timer;
    }

	private void setRotation(Transform targetTransform)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetTransform.position - enemyTransform.position);
        float oryginalX = enemyTransform.rotation.x;
        float oryginalZ = enemyTransform.rotation.z;

        Quaternion finalRotation = Quaternion.Slerp(enemyTransform.rotation, targetRotation, 5.0f * Time.deltaTime);
        finalRotation.x = oryginalX;
        finalRotation.z = oryginalZ;
        enemyTransform.rotation = finalRotation;
    }
}