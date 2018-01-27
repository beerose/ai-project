using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Shot;
    public GameObject Spell;
    public float MaxMana;
    public float Mana;
    public float ManaRegen;
    public float Damage;
    public float DamageModifier;
    public float FireDelay;
    public float FireDelayModifier;
    public float BulletSpeed;
    public float BulletSpeedModifier;

    private float nextFire;
    private string ShooterTag;
    private AudioSource aud;
    private PlayerHealth HP;
    private GameObject defaultShot;
    private GameObject defaultSpell;


    void Start()
    {
        defaultShot = Shot;
        defaultSpell = Spell;
        ShooterTag = transform.parent.tag;
        aud = GetComponent<AudioSource>();
        if (transform.parent.tag.Equals("Player"))
        {
            HP = GetComponentInParent<PlayerHealth>();
            EquipmentManager.Instance.OnEquipmentChangedCallback += onEquipmentChangedCallback;
        }
        InvokeRepeating("manaRegen", 0, 1f);
    }

    void manaRegen()
    {
        Mana += ManaRegen;

        if (Mana > MaxMana) Mana = MaxMana;
    }

    void onEquipmentChangedCallback(Equipment newItem, Equipment oldItem)
    {
        if (oldItem != null)
        {
            if ((int) oldItem.EquipSlot == 0)
            {
                DamageModifier -= oldItem.DamageModifier;
                FireDelayModifier -= oldItem.FireDelayModifier;
                BulletSpeedModifier -= oldItem.BulletSpeedModifier;
                if (oldItem.Shot != null) Shot = defaultShot;
            }
            if ((int) oldItem.EquipSlot == 1)
            {
                HP.m_HealthModifier -= oldItem.HealthModifier;
                HP.SetHealthUI();
                HP.UpdateHP();
            }

            if ((int) oldItem.EquipSlot == 2)
            {
                if (oldItem.Spell != null) Spell = defaultSpell;
            }
        }

        if (newItem != null)
        {
            if ((int) newItem.EquipSlot == 0)
            {
                DamageModifier += newItem.DamageModifier;
                FireDelayModifier += newItem.FireDelayModifier;
                BulletSpeedModifier += newItem.BulletSpeedModifier;
                if (newItem.Shot != null) Shot = newItem.Shot;
            }
            if ((int) newItem.EquipSlot == 1)
            {
                HP.m_HealthModifier += newItem.HealthModifier;
                HP.SetHealthUI();
                HP.UpdateHP();
            }
            if ((int) newItem.EquipSlot == 2)
            {
                if (newItem.Spell != null) Spell = newItem.Spell;
            }
        }

        transform.parent.GetComponentInChildren<PlayerAnimatorController>().ChangeAnimAttackSpeed();
    }


    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireDelay + FireDelayModifier;
            ShotMover bullet = Instantiate(Shot, transform.position, transform.rotation).GetComponent<ShotMover>();
            bullet.SetShooterTag(ShooterTag);
            bullet.Damage = Damage + DamageModifier;
            bullet.Speed = BulletSpeed + BulletSpeedModifier;
            aud.Play();
        }
    }

    public void Cast()
    {
        if (Spell != null)
        {
            float manaCost = Spell.GetComponent<SpellBehaviour>().ManaCost;
            if (Mana >= manaCost && Time.time > nextFire)
            {
                nextFire = Time.time + FireDelay + FireDelayModifier;
                Mana -= manaCost;
                SpellBehaviour spell = Instantiate(Spell, transform.position, transform.rotation)
                    .GetComponent<SpellBehaviour>();
                spell.SetShooterTag(ShooterTag);
                Debug.Log("Casting " + spell.name);
            }
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
        return Shot.GetComponent<ShotMover>().Damage;
    }

    public float GetLifeTime()
    {
        return Shot.GetComponent<ShotMover>().LifeTime;
    }
}