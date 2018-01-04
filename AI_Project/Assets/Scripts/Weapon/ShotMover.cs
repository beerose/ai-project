using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMover : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject Shooter;
    public float Speed;
    public float LifeTime;
    public float power = 1;
    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * Speed;
        Destroy(gameObject, LifeTime);
    }

    void OnTriggerEnter(Collider col)
    {
		if (col.name != Shooter.name && !col.tag.Equals("Enemy Eyeshot"))
        {
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}