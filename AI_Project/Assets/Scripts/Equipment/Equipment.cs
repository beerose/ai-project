using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot EquipSlot;

    public float DamageModifier; //Weapon
    public float BulletSpeedModifier; //Weapon
    public float FireDelayModifier; //Weapon
    public GameObject Shot; //Weapon
    public int ShotNumberModifier; //Weapon

    public float HealthModifier; //Armor


    public GameObject Spell; //Spell

    public override void Use()
    {
        base.Use();
        RemoveFromInventory();
        EquipmentManager.Instance.Equip(this);
    }

    public override void Unequip()
    {
        base.Unequip();
        EquipmentManager.Instance.Unequip(this);
    }

    public int GetPowerLVL()
    {
        int power = 0;
        if ((int) EquipSlot == 0) //Weapon
        {
            power = (int) ((DamageModifier / FireDelayModifier * ShotNumberModifier + BulletSpeedModifier/10)/3);
        }
        if ((int) EquipSlot == 1) //Armor
        {
            power = (int) (HealthModifier / 50);
        }
        if ((int) EquipSlot == 2) //Spell
        {
            power = (int)(Spell.GetComponent<SpellBehaviour>().Damage / Spell.GetComponent<SpellBehaviour>().ManaCost);
        }
        return power;
    }

    public void Build(string name, Sprite icon, EquipmentSlot equipmentSlot, float dmgMod, float bulletSpeedMod,
        float fireDelayMod, GameObject shot, int shotNmod, float HPMod, GameObject spell)
    {
        this.name = name;
        this.icon = icon;
        this.EquipSlot = equipmentSlot;
        this.DamageModifier = dmgMod;
        this.BulletSpeedModifier = bulletSpeedMod;
        this.FireDelayModifier = fireDelayMod;
        this.Shot = shot;
        this.ShotNumberModifier = shotNmod;
        this.HealthModifier = HPMod;
        this.Spell = spell;
    }
}

public enum EquipmentSlot
{
    Weapon,
    Armor,
    Spell
}