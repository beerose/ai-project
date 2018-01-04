using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBuilder : MonoBehaviour
{
    public GameObject RoomPrefab;

    public struct RoomModel
    {
        public Vector2 size;
        public Vector3 position;

        public RoomModel(Vector2 _size, Vector3 _position)
        {
            size = _size;
            position = _position;
        }
    }

    public RoomModel roomModel;
    public RoomModel GetModel 
    {
        get
        {
            return roomModel;
        }
        set
        {
            SetModel(value);
        }
    }

    public void SetModel(RoomModel _roomModel)
    {
        roomModel = _roomModel;
    }

    public void CollectRooms() {
		Random.InitState(1211);
        Debug.Log(Random.value);
    }

    public void CreateRoom(Vector2 _size, Vector3 position) {
        GameObject room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity);
        room.transform.parent = transform;
        var x = transform.GetComponentsInChildren<BuildableRoomBehaviour>();
        x[x.Length - 1].SetSize(_size);
		x[x.Length - 1].SetPosition(position);
    }
}
