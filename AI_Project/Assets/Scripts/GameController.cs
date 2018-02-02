using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instace;

    public GameObject YouWinUI;
    public GameObject GameOverUI;
    public GameObject OptionsUI;

    public GameObject Boss;

    private EnemiesCollector EC;

    private GameObject currentBoard;

    private bool youWin;
    private bool gameOver;


    void Start()
    {
        Instace = FindObjectOfType<GameController>();
        gameOver = false;
        //RemoveDoors();
        EC = GameObject.FindGameObjectWithTag("EnemiesCollector").GetComponent<EnemiesCollector>();
        Invoke("bossSpawn", 1); //temporary
    }

    private void bossSpawn() //temporary
    {
        var boards = GameObject.FindGameObjectsWithTag("Board");
        int id = new System.Random().Next(boards.Length);
        int i = 0;
        for (i = 0; i < 50; i++)
        {
            if (boards[id].name.Equals(currentBoard.name))
            {
                id = new System.Random(i).Next(boards.Length);
            }
            else
                break;
        }
        if (i != 50) Instantiate(Boss, boards[id].transform.position, boards[id].transform.rotation);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            var iui = GameObject.Find("Inventory UI");
            var eui = GameObject.Find("Equipment UI");
            if (iui != null) iui.SetActive(false);
            if (eui != null) eui.SetActive(false);
            if (iui == null && eui == null) OptionsUI.SetActive(!OptionsUI.activeSelf);
        }
    }

    public void ChangeBoard(GameObject newBoard)
    {
        if (Time.timeSinceLevelLoad > 0.5f) EC.LoadEnemiesInRoom(newBoard.name);
        currentBoard = newBoard;
    }

    public GameObject GetCurrentBoard()
    {
        return currentBoard;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void WinGame()
    {
        if (!gameOver)
        {
            youWin = true;
            YouWinUI.SetActive(true);
        }
        else
        {
            youWin = true;
        }
    }

    public void GameOver()
    {
        if (!youWin)
        {
            gameOver = true;
            GameOverUI.SetActive(true);
        }
        else
        {
            gameOver = true;
        }
    }

    public bool getGameStatus()
    {
        return gameOver;
    }

    /*private void RemoveDoors() //temporary
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
            if (kill) door1.GetComponent<DoorBehaviour>().KYS();
        }
    }*/

    /*
    float Distance(float x1, float x2, float z1, float z2) //temporary
    {
        return Mathf.Sqrt((x2 - x1) * (x2 - x1) + (z2 - z1) * (z2 - z1));
    }*/
}