using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentGenerator : MonoBehaviour
{
    #region Singleton

    public static EquipmentGenerator Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    public delegate void ProcessEnd(List<Equipment> EQ);

    public ProcessEnd ProcessEndCallback;

    public int InitNum;
    public int SelectNum;
    public int MaxPowerLVL;
    public int MinPowerLVL;

    public string[] Prefix;
    public string[] Sufix;

    public Sprite[] Icons;

    public Vector2 DamageModifier; //Weapon
    public Vector2 BulletSpeedModifier; //Weapon
    public Vector2 FireDelayModifier; //Weapon
    public GameObject[] Shot; //Weapon
    public int NumberOfShots; //Weapon

    public Vector2 HealthModifier; //Armor

    public GameObject[] Spell; //Spell

    private List<Equipment> EQ = new List<Equipment>();

    private int seed;

    private void Start()
    {
        seed = GameObject.Find("Dungeon").GetComponent<BuildOnStart>().Seed;
        Invoke("Generate", 1.5f);
    }

    private void Generate()
    {
        Initialization();

        for (int i = 0; i < SelectNum; i++)
        {
            EQ.Add(Mutation(Crossover(Selection(i), Selection(i * 17), i * 23)));
            //mods are "i" * my random numbers + seed
        }

        Termination();

        if (ProcessEndCallback != null)
        {
            ProcessEndCallback.Invoke(EQ);
        }

        LoadingBar.Instance.Progress += 1;
    }

    private void Initialization()
    {
        for (int i = 0; i < InitNum; i++)
        {
            Random.InitState(seed + i);

            string name = Prefix[Random.Range(0, Prefix.Length)] + " of " + Sufix[Random.Range(0, Sufix.Length)];
            Sprite icon = Icons[Random.Range(0, Icons.Length)];

            EquipmentSlot equipmentSlot = (EquipmentSlot) Random.Range(0, 3);

            float dmgMod = Random.Range(DamageModifier.x, DamageModifier.y);
            float bulletSpeedMod = Random.Range(BulletSpeedModifier.x, BulletSpeedModifier.y);
            float fireDelayMod = Random.Range(FireDelayModifier.x, FireDelayModifier.y);
            GameObject shot = Shot[Random.Range(0, Shot.Length)];
            int shotNmod = NumberOfShots;

            float HPMod = Random.Range(HealthModifier.x, HealthModifier.y);

            GameObject spell = Spell[Random.Range(0, Spell.Length)];

            Equipment init = ScriptableObject.CreateInstance<Equipment>();
            init.Build(name, icon, equipmentSlot, dmgMod, bulletSpeedMod, fireDelayMod, shot, shotNmod, HPMod,
                spell);

            EQ.Add(init);
        }
    }

    private Equipment Selection(int mod)
    {
        Random.InitState(seed + mod);
        return EQ[Random.Range(0, EQ.Count)];
    }

    private Equipment Crossover(Equipment p1, Equipment p2, int mod)
    {
        Equipment c = ScriptableObject.CreateInstance<Equipment>();
        Random.InitState(seed + mod);

        string name = p1.name.Split(null)[0] + " of " + p2.name.Split(null)[2];
        Sprite icon = Icons[Random.Range(0, Icons.Length)];

        EquipmentSlot equipmentSlot = (EquipmentSlot) Random.Range(0, 3);

        float dmgMod = (p1.DamageModifier + p2.DamageModifier) * 0.7f;
        float bulletSpeedMod = (p1.BulletSpeedModifier + p2.BulletSpeedModifier) * 0.7f;
        float fireDelayMod = (p1.FireDelayModifier + p2.FireDelayModifier) * 0.7f;

        GameObject shot = p1.Shot;
        int shotNmod = p1.ShotNumberModifier;
        if (p1.Shot.name.Equals(p2.Shot.name)) shotNmod += NumberOfShots;

        float HPMod = (p1.HealthModifier + p2.HealthModifier) * 0.7f;

        int id = Random.Range(0, 2);
        GameObject spell = p1.Spell;
        if (id == 1) spell = p2.Spell;

        c.Build(name, icon, equipmentSlot, dmgMod, bulletSpeedMod, fireDelayMod, shot, shotNmod, HPMod,
            spell);

        return c;
    }

    private Equipment Mutation(Equipment c)
    {
        Random.InitState(seed + c.GetInstanceID());
        switch (Random.Range(0, 12))
        {
            case 0:
                c.DamageModifier = Random.Range(DamageModifier.x, DamageModifier.y);
                break;
            case 1:
                c.BulletSpeedModifier = Random.Range(BulletSpeedModifier.x, BulletSpeedModifier.y);
                break;
            case 2:
                c.FireDelayModifier = Random.Range(FireDelayModifier.x, FireDelayModifier.y);
                break;
            case 3:
                c.Shot = Shot[Random.Range(0, Shot.Length)];
                break;
            case 4:
                c.ShotNumberModifier = NumberOfShots;
                break;
            case 5:
                c.HealthModifier = Random.Range(HealthModifier.x, HealthModifier.y);
                break;
            case 6:
                c.Spell = Spell[Random.Range(0, Spell.Length)];
                break;
            default:
                break;
        }
        return c;
    }

    private void Termination()
    {
        for (int i = 0; i < EQ.Count; i++)
        {
            if (EQ[i].GetPowerLVL() > MaxPowerLVL || EQ[i].GetPowerLVL() <= MinPowerLVL)
            {
                if ((int) EQ[i].EquipSlot != 0)
                    Debug.Log("Delete item with power lvl: " + EQ[i].GetPowerLVL() + " hpMod: " + (int) EQ[i].HealthModifier);
                EQ.RemoveAt(i);
                i--;
            }
            else
            {
                if (EQ[i].ShotNumberModifier > 1 && (int) EQ[i].EquipSlot == 0)
                    Debug.Log("Weapon with " + EQ[i].ShotNumberModifier + " shots Power: " + EQ[i].GetPowerLVL() +
                              " DMG: " + EQ[i].DamageModifier + " Delay " + EQ[i].FireDelayModifier);
            }
        }
    }
}