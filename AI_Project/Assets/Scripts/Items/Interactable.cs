using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            Interact();
        }
       
    }
}