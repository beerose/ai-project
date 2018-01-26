using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public float fireDelay;
    public float speed;
    public float power;
    public float PowerModifier;
    private float nextFire;
    private string ShooterTag;
    private AudioSource aud;
    private PlayerHealth HP;

    void Start()
    {
        ShooterTag = transform.parent.tag;
        aud = GetComponent<AudioSource>();
        if (transform.parent.tag.Equals("Player"))
        {
            HP = GetComponentInParent<PlayerHealth>();
            EquipmentManager.Instance.OnEquipmentChangedCallback += onEquipmentChangedCallback;
        }
    }

    void onEquipmentChangedCallback(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            HP.m_HealthModifier += newItem.HealthModifier;
            HP.SetHealthUI();
            PowerModifier += newItem.DamageModifier;
        }

        if (oldItem != null)
        {
            HP.m_HealthModifier -= oldItem.HealthModifier;
            HP.SetHealthUI();
            PowerModifier -= oldItem.DamageModifier;
        }
    }


    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireDelay;
            ShotMover bullet = Instantiate(shot, transform.position, transform.rotation).GetComponent<ShotMover>();
            bullet.SetShooterTag(ShooterTag);
            bullet.Power = power + PowerModifier;
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