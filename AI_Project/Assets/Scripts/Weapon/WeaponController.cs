using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public float fireRate;
    private Transform shotSpawn;
    private float nextFire;
    private string ShooterName;
    private AudioSource aud;

    void Start()
    {
        ShooterName = OldestParent();
        shotSpawn = GetComponentInChildren<Transform>();
        aud = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ShotMover bullet = Instantiate(shot, shotSpawn.position, shotSpawn.rotation).GetComponent<ShotMover>();
            bullet.SetShooterName(ShooterName);
            aud.Play();
        }
    }

    private string OldestParent()
    {
        Transform t = transform;
        while (t.parent != null)
        {
            t = t.parent;
        }
        return t.name;
    }

    public float GetPower()
    {
        return shot.GetComponent<ShotMover>().Power;
    }

    public float GetLifeTime()
    {
        return shot.GetComponent<ShotMover>().LifeTime;
    }

    public float GetSpeed()
    {
        return shot.GetComponent<ShotMover>().Speed;
    }
}