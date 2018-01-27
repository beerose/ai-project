using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public GameObject PickUPeffect;

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
            Instantiate(PickUPeffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}