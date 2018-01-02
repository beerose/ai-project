﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    North, East, South, West
}

public class DoorBehaviour : MonoBehaviour
{
    public const int teleportationStrength = 3;
    public Direction direction;
    public bool isOpen = true;

    private GameObject nextBoard;
    private float newBoundaryXMin;
    private float newBoundaryXMax;
    private float newBoundaryZMin;
    private float newBoundaryZMax;

    bool AgentCanPassThroughDoor(Transform agent) 
    {
        return agent.tag == "Player";
    }

    void OnCollisionEnter(Collision collision)
    {
        var agent = collision.transform;
        Debug.Log(agent);

        if (isOpen && AgentCanPassThroughDoor(agent))
        {
            Vector3 translation;
            switch (direction)
            {
                case Direction.North:
                    translation = Vector3.forward;
                    break;
                case Direction.East:
                    translation = Vector3.left;
                    break;
                case Direction.South:
                    translation = Vector3.back;
                    break;
                case Direction.West:
                    translation = Vector3.right;
                    break;
                default:
                    throw new UnityException("Switch default");
            }
            agent.transform.position += translation * teleportationStrength;

            if (agent.tag == "Player")
            {
                var player = agent.GetComponent<Player.PlayerController>();
                player.OnEnterNewRoom();
            }
        }
    }
}