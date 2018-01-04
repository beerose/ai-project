using UnityEngine;
using System.Collections;

public class DungeonBehaviour : MonoBehaviour
{
    public GameObject Player;

    void OnStart() {
        if (Player == null) Player = GameObject.FindWithTag("Player");
    }
}
