using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    //private AudioSource audioSource;

    void Start ()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void Fire ()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //audioSource.Play();
        }
    }

}
