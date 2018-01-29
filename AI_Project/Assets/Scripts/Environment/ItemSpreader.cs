using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpreader : MonoBehaviour
{
    public GameObject[] Items;
    public int ChanceToGetItem = 30;

    void Start()
    {
        Invoke("active", 1.0f);
    }

    private void active()
    {
        if (!GameController.Instace.GetCurrentBoard().name.Equals(gameObject.name))
        {
            System.Random randomNumber = new System.Random(GetInstanceID());
            int id = randomNumber.Next(0, Items.Length);
            int chance = randomNumber.Next(100);
            if(chance<ChanceToGetItem)Instantiate(Items[id], transform.position,Items[id].transform.rotation);
        }
    }
}