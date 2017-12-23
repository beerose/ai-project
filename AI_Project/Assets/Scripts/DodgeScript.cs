using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeScript : MonoBehaviour {

    public float DodgeMaxVelocity;
    public float Smoothing;
    public Vector2 WaitToStartDodge;
    public Vector2 DodgeTime;
    public Vector2 AfterDodgeWait;
    
    private float targetDodgeX;
    private float targetDodgeZ;
    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(EvadeX());
        StartCoroutine(EvadeZ());
    }

    IEnumerator EvadeX()
    {
        yield return new WaitForSeconds(Random.Range(WaitToStartDodge.x,WaitToStartDodge.y));

        while(true)
        {
            targetDodgeX = DodgeMaxVelocity;
            yield return new WaitForSeconds(Random.Range(DodgeTime.x,DodgeTime.y));
            targetDodgeX = 0;
            yield return new WaitForSeconds(Random.Range(AfterDodgeWait.x,AfterDodgeWait.y));
            targetDodgeX = -DodgeMaxVelocity;
            yield return new WaitForSeconds(Random.Range(DodgeTime.x, DodgeTime.y));
            targetDodgeX = 0;
            yield return new WaitForSeconds(Random.Range(AfterDodgeWait.x, AfterDodgeWait.y));
        }
    }

    IEnumerator EvadeZ()
    {
        yield return new WaitForSeconds(Random.Range(WaitToStartDodge.x, WaitToStartDodge.y));

        while (true)
        {
            targetDodgeZ = DodgeMaxVelocity;
            yield return new WaitForSeconds(Random.Range(DodgeTime.x, DodgeTime.y));
            targetDodgeZ = 0;
            yield return new WaitForSeconds(Random.Range(AfterDodgeWait.x, AfterDodgeWait.y));
            targetDodgeZ = -DodgeMaxVelocity;
            yield return new WaitForSeconds(Random.Range(DodgeTime.x, DodgeTime.y));
            targetDodgeZ = 0;
            yield return new WaitForSeconds(Random.Range(AfterDodgeWait.x, AfterDodgeWait.y));
        }
    }

    void FixedUpdate ()
    {
        float newDodgeX = Mathf.MoveTowards(rb.velocity.x, targetDodgeX, Time.deltaTime * Smoothing);
        float newDodgeZ = Mathf.MoveTowards(rb.velocity.z, targetDodgeZ, Time.deltaTime * Smoothing);
        rb.velocity = new Vector3(newDodgeX, 0.0f, newDodgeZ);
    }
}
