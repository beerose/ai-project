using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoThroughDoor : MonoBehaviour
{
    public bool status;
    private GameObject nextBoard;
    private GameObject player;
    private GameController GC;
    private float newBoundaryXMin;
    private float newBoundaryXMax;
    private float newBoundaryZMin;
    private float newBoundaryZMax;

    void Start()
    {
        GC = FindObjectOfType<GameController>();
        status = true;
        player = GameObject.FindGameObjectWithTag("Player");
        NextBoardSearch();
        if(nextBoard==null)Destroy(gameObject);
        else NewBoundaryPos();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player" && status)
        {
            ChangeBoard();
            if (transform.parent.name == "Door U")
            {
                player.transform.position = new Vector3(0, 0, 3)+ player.transform.position;
            }
            if (transform.parent.name == "Door D")
            {
                player.transform.position = new Vector3(0, 0, -3) + player.transform.position;
            }
            if (transform.parent.name == "Door L")
            {
                player.transform.position = new Vector3(-3, 0, 0) + player.transform.position;
            }
            if (transform.parent.name == "Door R")
            {
                player.transform.position = new Vector3(3, 0, 0) + player.transform.position;
            }
        }
    }

    //lepiej aby drzwi mialy referencje do kolejnych drzwi, do zmiany bo to ma O(n^2)
    void NextBoardSearch()
    {
        float closest = 4;
        foreach (var door in GameObject.FindGameObjectsWithTag("Door"))
        {
            float dist = Distance(transform.parent.position.x, door.transform.position.x, transform.parent.position.z,
                door.transform.position.z);
            if (dist > 0 && dist < closest)
            {
                closest = dist;
                nextBoard = door.transform.parent.gameObject;
            }
        }
    }

    void NewBoundaryPos()
    {
        newBoundaryXMin = Mathf.Ceil(nextBoard.transform.Find("Walls").Find("Wall L").position.x);
        newBoundaryXMax = Mathf.Floor(nextBoard.transform.Find("Walls").Find("Wall R").position.x);
        newBoundaryZMin = Mathf.Ceil(nextBoard.transform.Find("Walls").Find("Wall D").position.z);
        newBoundaryZMax = Mathf.Floor(nextBoard.transform.Find("Walls").Find("Wall U").position.z);
    }

    float Distance(float x1, float x2, float z1, float z2)
    {
        return Mathf.Sqrt((x2 - x1) * (x2 - x1) + (z2 - z1) * (z2 - z1));
    }

    void ChangeBoard()
    {
        PlayerController.Boundary boundary = player.GetComponent<PlayerController>().boundary;
        boundary.xMin = newBoundaryXMin;
        boundary.xMax = newBoundaryXMax;
        boundary.zMin = newBoundaryZMin;
        boundary.zMax = newBoundaryZMax;
        GC.currentBoard = nextBoard;
        GC.SetBoardChange();
    }
}