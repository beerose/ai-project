using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform ItemsParent;
    public GameObject equipmentUI;
    private EquipmentManager Equipment;
    private InventorySlot[] slots;

    void Start()
    {
        Equipment = EquipmentManager.Instance;
        Equipment.OnEquipmentChangedCallback += UpdateUI;

        slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Equipment"))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
    }

    void UpdateUI(Equipment newItem, Equipment oldItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (oldItem != null && i == (int) newItem.EquipSlot)
            {
                slots[i].ClerSlot();
            }
            if (newItem != null && i == (int) newItem.EquipSlot)
            {
                slots[i].AddItem(newItem);
            }
            
        }
    }
}