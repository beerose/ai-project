using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewRooms : MonoBehaviour
{
    private TMP_InputField rooms;

    void Start()
    {
        rooms = GetComponent<TMP_InputField>();
    }

    public void RoomsChange()
    {
        if (rooms.text.Equals("")) GameController.Instance.Rooms = BuildOnStart.Instance.Rooms;
        else GameController.Instance.Rooms = int.Parse(rooms.text);
    }
}