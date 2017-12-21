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
    private GameObject lastBoard;
    private bool boardChange = true;
    private GameObject player;

    void Start()
    {
        Instace = FindObjectOfType<GameController>();
        player = GameObject.FindWithTag("Player");
        lastBoard = currentBoard;
        hideOtherBoards();
    }

    void Update()
    {
        if (boardChange)
        {
            if (currentBoard.CompareTag("Board")) mainCamera.target = currentBoard.transform;
            else mainCamera.target = player.transform;
            hideLastBoard();
            lastBoard = currentBoard;
            boardChange = false;
        }
    }

    public void SetBoardChange()
    {
        boardChange = true;
    }

    private void hideLastBoard()
    {
        foreach (var VAR in lastBoard.GetComponentsInChildren<Transform>())
        {
            VAR.gameObject.layer = 8;
        }
        foreach (var VAR in currentBoard.GetComponentsInChildren<Transform>())
        {
            VAR.gameObject.layer = 0;
        }
    }

    private void hideOtherBoards()
    {
        foreach (var VARIABLE in GameObject.FindGameObjectsWithTag("Board"))
        {
            if (VARIABLE != currentBoard)
            {
                foreach (var VAR in VARIABLE.GetComponentsInChildren<Transform>())
                {
                    VAR.gameObject.layer = 8;
                }
            }
        }
        foreach (var VARIABLE in GameObject.FindGameObjectsWithTag("Board 2Z"))
        {
            if (VARIABLE != currentBoard)
            {
                foreach (var VAR in VARIABLE.GetComponentsInChildren<Transform>())
                {
                    VAR.gameObject.layer = 8;
                }
            }
        }
        foreach (var VARIABLE in GameObject.FindGameObjectsWithTag("Board 3Z"))
        {
            if (VARIABLE != currentBoard)
            {
                foreach (var VAR in VARIABLE.GetComponentsInChildren<Transform>())
                {
                    VAR.gameObject.layer = 8;
                }
            }
        }
        foreach (var VARIABLE in GameObject.FindGameObjectsWithTag("Board 2Z2X"))
        {
            if (VARIABLE != currentBoard)
            {
                foreach (var VAR in VARIABLE.GetComponentsInChildren<Transform>())
                {
                    VAR.gameObject.layer = 8;
                }
            }
        }
    }
}