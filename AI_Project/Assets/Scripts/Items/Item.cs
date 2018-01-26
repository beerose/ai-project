using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + name);
    }

    public virtual void Unequip()
    {
        // Unequip the item
        // Something might happen

        Debug.Log("Unequip " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}