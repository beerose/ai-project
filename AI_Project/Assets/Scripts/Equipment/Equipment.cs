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

    public void  Build(string name, Sprite icon, EquipmentSlot equipmentSlot, float dmgMod, float bulletSpeedMod,
        float fireDelayMod, GameObject shot, float HPMod, GameObject spell)
    {
        this.name = name;
        this.icon = icon;
        this.EquipSlot = equipmentSlot;
        this.DamageModifier = dmgMod;
        this.BulletSpeedModifier = bulletSpeedMod;
        this.FireDelayModifier = fireDelayMod;
        this.Shot = shot;
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