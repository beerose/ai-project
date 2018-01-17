using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instace;

    public GameObject GameOverUI;

    //public CameraFollow mainCamera;
    private GameObject currentBoard;

    //public GameObject Boards;
    //public GameObject Board;
    //private GameObject lastBoard;
    private bool boardChange = true;

    //private GameObject player;
    //private Vector3 here;
    private bool gameOver;

    void Start()
    {
        Instace = FindObjectOfType<GameController>();
        gameOver = false;
        //player = GameObject.FindWithTag("Player");
        //lastBoard = currentBoard;
        //here = new Vector3(0, 0, 16);
        //Instantiate(Board, here, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKey("escape")) Application.Quit();
        if (boardChange)
        {
            //if (currentBoard.CompareTag("Board")) mainCamera.target = currentBoard.transform;
            //else mainCamera.target = player.transform;
            //lastBoard = currentBoard;
            boardChange = false;
        }
    }

    public void ChangeBoard(GameObject newBoard)
    {
        currentBoard = newBoard;
        boardChange = true;
    }

    public GameObject GetCurrentBoard()
    {
        return currentBoard;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameOver()
    {
        gameOver = true;
        GameOverUI.SetActive(true);
    }

    public bool getGameStatus()
    {
        return gameOver;
    }
}