using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instace;

    public GameObject InventoryUI;
    public GameObject EquipmentUI;
    public GameObject YouWinUI;
    public GameObject GameOverUI;
    public GameObject OptionsUI;
    public GameObject ControlsUI;
    public GameObject LoadingScreenUI;

    public GameObject Boss;

    private EnemiesCollector EC;

    private GameObject currentBoard;

    private bool youWin;
    private bool gameOver;
    private bool pause;
    private bool loading;

    void Start()
    {
        Instace = FindObjectOfType<GameController>();
        youWin = false;
        gameOver = false;
        pause = true;
        loading = true;
        //RemoveDoors();
        EC = GameObject.FindGameObjectWithTag("EnemiesCollector").GetComponent<EnemiesCollector>();
        Invoke("bossSpawn", 1); //temporary
        LoadingScreenUI.SetActive(true);
        LoadingBar.Instance.Progress += 1;
    }

    public void GameLoaded()
    {
        pause = false;
        loading = false;
        LoadingScreenUI.SetActive(false);
    }

    private void bossSpawn() //temporary
    {
        var boards = GameObject.FindGameObjectsWithTag("Board");
        int id = new System.Random().Next(boards.Length);
        int i;
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
        LoadingBar.Instance.Progress += 1;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!(InventoryUI.activeSelf || EquipmentUI.activeSelf || YouWinUI.activeSelf || GameOverUI.activeSelf ||
                  ControlsUI.activeSelf || loading))
            {
                OptionsUI.SetActive(!OptionsUI.activeSelf);
                if (OptionsUI.activeSelf) Time.timeScale = 0;
                else Time.timeScale = 1;
                pause = !pause;
            }
            InventoryUI.SetActive(false);
            EquipmentUI.SetActive(false);
            YouWinUI.SetActive(false);
            GameOverUI.SetActive(false);
            ControlsUI.SetActive(false);
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
        Time.timeScale = 1;
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

    public bool GetGameOver()
    {
        return gameOver;
    }

    public bool GetPause()
    {
        return pause;
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