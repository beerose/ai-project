using System.Collections;
using System.Collections.Generic;
using Dungeon;
using UnityEngine;

public class BuildOnStart : MonoBehaviour
{

    public Vector2 Rooms;

	void Start ()
	{
	    DungeonBuilder dungeon = GetComponent<DungeonBuilder>();
	    
        dungeon.BuildDungeon(0,Random.Range(1111, 999999), Random.Range((int)Rooms.x, (int)Rooms.y));
	}
}
