using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public GameObject PickUPEffect;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.Instance.Add(item);
        if (wasPickedUp)
        {
            Instantiate(PickUPEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}