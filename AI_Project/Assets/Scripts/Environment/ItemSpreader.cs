using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpreader : MonoBehaviour
{
    public List<GameObject> WeaponModels;
    public List<GameObject> ArmorModels;
    public List<GameObject> SpellModels;
    public List<GameObject> PotionModels;
    public int ChanceToGetItem;

    private int seed;

    void Start()
    {
        GameObject ItemFolder = new GameObject();
        ItemFolder.transform.SetParent(transform);
        ItemFolder.name = "Items";
        seed = GameObject.Find("Dungeon").GetComponent<BuildOnStart>().Seed;
        EquipmentGenerator.Instance.ProcessEndCallback += RollItem;
    }

    private void RollItem(List<Equipment> EQ)
    {
        int count = 0;
        foreach (var room in GameObject.FindGameObjectsWithTag("Board"))
        {
            count++;
            if (!GameController.Instace.GetCurrentBoard().name.Equals(room.name))
            {
                Random.InitState(seed + count);
                int chance = Random.Range(0, 100);

                if (chance < ChanceToGetItem)
                {
                    GameObject obj = null;

                    if (chance < ChanceToGetItem / 2)
                    {
                        obj = Instantiate(PotionModels[Random.Range(0, PotionModels.Count)]);
                    }
                    else
                    {
                        Equipment e = EQ[Random.Range(0, EQ.Count)];

                        if ((int) e.EquipSlot == 0) //Weapon
                        {
                            obj = Instantiate(WeaponModels[Random.Range(0, WeaponModels.Count)]);
                        }
                        if ((int) e.EquipSlot == 1) //Armor
                        {
                            obj = Instantiate(ArmorModels[Random.Range(0, ArmorModels.Count)]);
                        }
                        if ((int) e.EquipSlot == 2) //Spell
                        {
                            obj = Instantiate(SpellModels[Random.Range(0, SpellModels.Count)]);
                        }
                        
                        obj.GetComponent<ItemPickup>().item = e;
                        obj.name = e.name;
                    }
                    obj.transform.position = room.transform.position;
                    obj.transform.SetParent(transform.Find("Items"));
                }
            }
        }
    }
}