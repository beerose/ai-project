using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button RemoveButton;
    private Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        RemoveButton.interactable = true;
    }

    public void ClerSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        RemoveButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.Remove(item);
    }
    
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void UnequipItem()
    {
        if (item != null)
        {
            item.Unequip();
        }
    }
}
