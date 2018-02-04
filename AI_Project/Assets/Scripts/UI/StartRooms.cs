using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartRooms : MonoBehaviour
{
    private TextMeshProUGUI rooms;

    void Start()
    {
        rooms = GetComponent<TextMeshProUGUI>();
        rooms.text = BuildOnStart.Instance.Rooms.ToString();
    }
}