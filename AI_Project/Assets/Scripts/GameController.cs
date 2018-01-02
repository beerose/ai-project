using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instace;
    public CameraFollow mainCamera;
    public GameObject currentBoard;
    public GameObject Boards;
    public GameObject Board;
    private GameObject lastBoard;
    private bool boardChange = true;
    private GameObject player;
    private Vector3 here;

    void Start()
    {
        Instace = FindObjectOfType<GameController>();
        player = GameObject.FindWithTag("Player");
        lastBoard = currentBoard;
        here = new Vector3(0, 0, 16);
        Instantiate(Board, here, Quaternion.identity);
    }

    void Update()
    {
        if (boardChange)
        {
            if (currentBoard.CompareTag("Board")) mainCamera.target = currentBoard.transform;
            else mainCamera.target = player.transform;
            lastBoard = currentBoard;
            boardChange = false;
        }
    }

    public void SetBoardChange()
    {
        boardChange = true;
    }
}