using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableRoomBehaviour : MonoBehaviour
{
    const int NORTH = 0;
    const int EAST = 1;
    const int SOUTH = 2;
    const int WEST = 3;

    public Transform[] Walls;
    public Transform[] Doors;
    public Transform Floor;
    public Transform Roof;

    private Transform[] CollectDescendants(string containerChildName) {
        var list = new List<Transform>();
        var container = transform.Find(containerChildName);
        foreach (Transform t in container)
        {
            list.Add(t);
        }
        return list.ToArray();
    }

    public void CollectDoorsAndWalls() {
        Floor = transform.Find("Floor");
        Roof = transform.Find("Roof");
        Walls = CollectDescendants("Walls");
        Doors = CollectDescendants("Doors");
    }

    private Vector2 _size;
    public Vector2 Size {
        get 
        {
            return _size;
        }
        set
        {
            SetSize(value);    
        }
    }

    private Vector3 _position;
    public Vector3 Position 
    {
        get
        {
            return _position;
        }
        set
        {
            SetPosition(value);
        }
    }


    public void SetSize(Vector2 size)
    {
        _size = size;

		var floorScale = Floor.localScale;
		floorScale.z = size.y / 10;
		floorScale.x = size.x / 10;
		Floor.localScale = floorScale;
		Roof.localScale = floorScale;

        var northScale = Walls[NORTH].localScale;
        northScale.x = size.x;
        Walls[NORTH].localScale = northScale;
        var northPos = Walls[NORTH].position;
        northPos.z = size.y / 2 - 0.25f;
        Walls[NORTH].position = northPos;

        var southScale = Walls[SOUTH].localScale;
        southScale.x = size.x;
        Walls[SOUTH].localScale = northScale;
        var southPos = Walls[SOUTH].position;
        southPos.z = -size.y / 2 + 0.25f;
        Walls[SOUTH].position = southPos;

        var westScale = Walls[WEST].localScale;
        westScale.x = size.y;
        Walls[WEST].localScale = westScale;
        var westPos = Walls[WEST].position;
        westPos.x = - size.x / 2 + .25f;
        Walls[WEST].position = westPos;

        var eastScale = Walls[EAST].localScale;
        eastScale.x = size.y;
        Walls[EAST].localScale = eastScale;
        var eastPos = Walls[EAST].position;
        eastPos.x = size.x / 2  - .25f;
        Walls[EAST].position = eastPos;

        SetDoors(size);
    }

    public void SetDoors(Vector2 size)
    {
        var northPos = Doors[NORTH].position;
        northPos.z = size.y / 2 - .6f;
        Doors[NORTH].position = northPos;

        var southPos = Doors[SOUTH].position;
        southPos.z = -size.y / 2 + .6f;
        Doors[SOUTH].position = southPos;

        var westPos = Doors[WEST].position;
        westPos.x = size.x / 2 - .6f;
        Doors[WEST].position = westPos;

        var eastPos = Doors[EAST].position;
        eastPos.x = -size.x / 2 + .6f;
        Doors[EAST].position = eastPos;

    }

    public void SetPosition(Vector3 position)
    {
        _position = position;
        transform.position = position;
    }


	void Awake ()
    {
	}
	
	void Update ()
    {	
	}
}
