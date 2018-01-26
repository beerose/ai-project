using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    public Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);

    public OnEquipmentChanged OnEquipmentChangedCallback;

    private Inventory inventory;

    void Start()
    {
        inventory = Inventory.Instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int) newItem.EquipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (OnEquipmentChangedCallback != null)
        {
            OnEquipmentChangedCallback.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(Equipment oldItem)
    {
        int slotIndex = (int)oldItem.EquipSlot;

        if (currentEquipment[slotIndex] != null)
        {
            if (inventory.Add(oldItem))
            {
                currentEquipment[slotIndex] = null;

                if (OnEquipmentChangedCallback != null)
                {
                    OnEquipmentChangedCallback.Invoke(null, oldItem);
                }
            }
        }
    }
}