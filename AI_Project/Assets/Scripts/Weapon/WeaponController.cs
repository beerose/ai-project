using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public float fireDelay;
	public float speed;
	public float power;
    private float nextFire;
    private string ShooterTag;
    private AudioSource aud;

    void Start()
    {
        ShooterTag = transform.parent.tag;
        aud = GetComponent<AudioSource>();
    }

	public void Fire()
    {
        if (Time.time > nextFire)
        {
			nextFire = Time.time + fireDelay;
            ShotMover bullet = Instantiate(shot, transform.position, transform.rotation).GetComponent<ShotMover>();
            bullet.SetShooterTag(ShooterTag);
			bullet.Power = power;
			bullet.Speed = speed;
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

	public bool isAvailable()
	{
		return Time.time > nextFire;
	}

    public float GetPower()
    {
        return shot.GetComponent<ShotMover>().Power;
    }

    public float GetLifeTime()
    {
        return shot.GetComponent<ShotMover>().LifeTime;
    }
}