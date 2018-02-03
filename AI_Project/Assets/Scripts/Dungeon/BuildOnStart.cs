using System.Collections;
using System.Collections.Generic;
using Dungeon;
using UnityEngine;

public class BuildOnStart : MonoBehaviour
{
    public int Seed;
    public int Rooms;
    
	void Start ()
	{
	    DungeonBuilder dungeon = GetComponent<DungeonBuilder>();
	    if(Seed == 0)Seed = Random.Range(1111, 999999);
	    if (Rooms == 0) Rooms = Random.Range(10, 40);

        dungeon.BuildDungeon(0,Seed, Rooms);
	}
}
