using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEnemiesDetector : MonoBehaviour
{
    private DoorBehaviour[] doors;
    private int numberOfEnemys;

    void Start()
    {
        doors = transform.parent.Find("Doors").GetComponentsInChildren<DoorBehaviour>();
        InvokeRepeating("Check", 0, 0.5f);
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("Boss")) numberOfEnemys++;
    }

    void Check()
    {
        foreach (DoorBehaviour door in doors)
        {
            door.OpenDoor();
        }
        if (numberOfEnemys > 0)
        {
            foreach (DoorBehaviour door in doors)
            {
                door.CloseDoor();
            }
        }
        numberOfEnemys = 0;
    }
}