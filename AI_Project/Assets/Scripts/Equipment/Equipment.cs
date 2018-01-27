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
}

public enum EquipmentSlot
{
    Weapon,
    Armor,
    Spell
}