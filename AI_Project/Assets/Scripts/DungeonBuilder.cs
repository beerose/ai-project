using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBuilder : MonoBehaviour
{
    public GameObject RoomPrefab;

    public void CreateRoom(Vector2 size) {
        var room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity);
    }
}
