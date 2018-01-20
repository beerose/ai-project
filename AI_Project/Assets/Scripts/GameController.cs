using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instace;

    public GameObject GameOverUI;
    public GameObject YouWinUI;


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
        RemoveDoors();
        //player = GameObject.FindWithTag("Player");
        //lastBoard = currentBoard;
        //here = new Vector3(0, 0, 16);
        //Instantiate(Board, here, Quaternion.identity);
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Boss").Length == 0 && Time.timeSinceLevelLoad > 3f) //temporary
        {
            YouWinUI.SetActive(true);
        }
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

    private void RemoveDoors() //temporary
    {
        foreach (var door1 in GameObject.FindGameObjectsWithTag("Door"))
        {
            bool kill = true;
            foreach (var door2 in GameObject.FindGameObjectsWithTag("Door"))
            {
                float dist = Distance(door1.transform.position.x, door2.transform.position.x,
                    door1.transform.position.z,
                    door2.transform.position.z);
                if (dist > 0 && dist < 4)
                {
                    kill = false;
                }
            }
            if(kill)door1.GetComponent<DoorBehaviour>().KYS();
        }
    }


    float Distance(float x1, float x2, float z1, float z2)
    {
        return Mathf.Sqrt((x2 - x1) * (x2 - x1) + (z2 - z1) * (z2 - z1));
    }
}