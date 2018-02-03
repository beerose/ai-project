using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Shot;
    public GameObject Spell;
    public float MaxMana = 35f;
    public float Mana = 35f;
    public float ManaRegen = 5f;
    public float Damage = 1f;
    public float DamageModifier = 0f;
    public float FireDelay = 0.5f;
    public float FireDelayModifier = 0f;
    public float SpellDelay = 1f;
    public float SpellDelayModifier = 0f;
    public float BulletSpeed = 5f;
    public float BulletSpeedModifier = 0f;
    public int NumberOfShots = 1;
    public int NumberOfShotsModifier = 0;

    private float nextFire;
    private float nextSpell;
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

            InvokeRepeating("manaRegen", 0.5f, 1f);

            Invoke("dmgUpdate", 0.5f);
        }
    }

    void dmgUpdate()
    {
        StatisticsUI.Instance.DPSupdate((Damage + DamageModifier) / (FireDelay + FireDelayModifier),
            BulletSpeed + BulletSpeedModifier, NumberOfShots + NumberOfShotsModifier);
        if (Spell != null)
            StatisticsUI.Instance.SpellUpdate(Spell.GetComponent<SpellBehaviour>().Damage,
                Spell.GetComponent<SpellBehaviour>().ManaCost);
        else StatisticsUI.Instance.SpellUpdate(0f, 0f);
    }

    void manaRegen()
    {
        Mana += ManaRegen;

        if (Mana > MaxMana) Mana = MaxMana;

        StatisticsUI.Instance.MPUpdate(Mana, MaxMana);
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
                NumberOfShotsModifier -= oldItem.ShotNumberModifier;
                if (oldItem.Shot != null) Shot = defaultShot;
            }
            if ((int) oldItem.EquipSlot == 1)
            {
                HP.m_HealthModifier -= oldItem.HealthModifier;
                HP.UpdateHP();
                HP.SetHealthUI();
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
                NumberOfShotsModifier += newItem.ShotNumberModifier;
                if (newItem.Shot != null) Shot = newItem.Shot;
            }
            if ((int) newItem.EquipSlot == 1)
            {
                HP.m_HealthModifier += newItem.HealthModifier;
                HP.UpdateHP();
                HP.SetHealthUI();
            }
            if ((int) newItem.EquipSlot == 2)
            {
                if (newItem.Spell != null) Spell = newItem.Spell;
            }
        }

        transform.parent.GetComponentInChildren<PlayerAnimatorController>().ChangeAnimAttackSpeed();

        dmgUpdate();
    }


    public void Fire()
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + FireDelay + FireDelayModifier;
            int sign = 1;
            for (int i = 0; i < NumberOfShots + NumberOfShotsModifier; i++)
            {
                sign *= -1;
                ShotMover bullet = Instantiate(Shot, transform.position, transform.rotation).GetComponent<ShotMover>();

                float correct = 0;
                if ((NumberOfShots + NumberOfShotsModifier) % 2 == 0) correct = 7.5f;
                bullet.transform.eulerAngles = new Vector3(0, sign * ((i + 1) / 2) * 15 - correct + transform.eulerAngles.y, 0);
                bullet.SetShooterTag(ShooterTag);
                bullet.Damage = Damage + DamageModifier;
                bullet.Speed = BulletSpeed + BulletSpeedModifier;
            }

            aud.Play();
        }
    }

    public void Cast()
    {
        if (Spell != null)
        {
            float manaCost = Spell.GetComponent<SpellBehaviour>().ManaCost;
            if (Mana >= manaCost && Time.time > nextSpell)
            {
                nextSpell = Time.time + SpellDelay + SpellDelayModifier;
                Mana -= manaCost;
                SpellBehaviour spell = Instantiate(Spell, transform.position, transform.rotation)
                    .GetComponent<SpellBehaviour>();
                spell.SetShooterTag(ShooterTag);
                StatisticsUI.Instance.MPUpdate(Mana, MaxMana);
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