using System.Collections;
using System.Collections.Generic;
using Dungeon;
using UnityEngine;

public class BuildOnStart : MonoBehaviour
{
    #region Singleton

    public static BuildOnStart Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one intence of BuildOnStart found!");
            return;
        }
        Instance = this;
    }

    #endregion

    public int Seed;
    public int Rooms;

    void Start()
    {
        DungeonBuilder dungeon = GetComponent<DungeonBuilder>();

        string S_R = Scenes.getParam("seed");
        if (!S_R.Equals(""))
        {
            Debug.Log(S_R);
            Seed = int.Parse(S_R.Split(null)[0]);
            Rooms = int.Parse(S_R.Split(null)[1]);
        }

        if (Seed == 0) Seed = Random.Range(1111, 999999);
        if (Rooms == 0) Rooms = Random.Range(10, 40);

        dungeon.BuildDungeon(0, Seed, Rooms);

        GameController.Instance.Seed = Seed;
        GameController.Instance.Rooms = Rooms;
    }
}

