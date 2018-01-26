using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot EquipSlot;

    public int HealthModifier;
    public int DamageModifier;

    public override void Use()
    {
        base.Use();
        RemoveFromInventory();
        EquipmentManager.Inststance.Equip(this);
    }
}

public enum EquipmentSlot
{
    Weapon,
    Armor,
    Spell
}