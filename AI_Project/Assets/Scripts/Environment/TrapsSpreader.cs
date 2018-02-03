using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsSpreader : MonoBehaviour
{
    public GameObject[] Traps;

    private List<Vector2> spawns = new List<Vector2>();

    void Start()
    {
        GameObject TrapsFolder = new GameObject();
        TrapsFolder.transform.SetParent(transform);
        TrapsFolder.name = "Traps";
        Invoke("spread", 1.0f);
    }

    private void spread()
    {
        int count = 0;
        foreach (var room in GameObject.FindGameObjectsWithTag("Board"))
        {
            spawns = new List<Vector2>();
            count++;
            if (!GameController.Instace.GetCurrentBoard().name.Equals(room.name))
            {
                Random.InitState(count);

                int id = Random.Range(0, Traps.Length);
                int trapSpreadRange;

                if (id == 0) trapSpreadRange = 5;
                else trapSpreadRange = 2;

                Vector3 roof = room.transform.Find("Roof").localScale;
                roof.x -= 0.01f;
                roof.y -= 0.01f;
                roof.z -= 0.01f;
                Vector4 board = new Vector4(-roof.x / 2 + 4, roof.x / 2 - 4, -roof.z / 2 + 4, roof.z / 2 - 4);

                int iter = Mathf.RoundToInt(roof.x * roof.z / 40);
                if (roof.x < 10 || roof.z < 10) iter = 0;
                for (int i = 0; i < iter; i++)
                {
                    bool restart = false;
                    int restartCount = 0;

                    Random.InitState(restartCount);
                    Vector2 spawn = new Vector2(Random.Range(board.x, board.y), Random.Range(board.z, board.w));
                    foreach (var sp in spawns)
                    {
                        if (Vector2.Distance(spawn, sp) < trapSpreadRange)
                        {
                            restart = true;
                            break;
                        }
                    }
                    while (restart)
                    {
                        restart = false;
                        Random.InitState(restartCount);
                        spawn = new Vector2(Random.Range(board.x, board.y), Random.Range(board.z, board.w));
                        foreach (var sp in spawns)
                        {
                            if (Vector2.Distance(spawn, sp) < trapSpreadRange)
                            {
                                restart = true;
                                restartCount++;
                                if (restartCount == 50)
                                {
                                    restart = false;
                                    restartCount = -1;
                                }
                                break;
                            }
                        }
                    }
                    if (restartCount != -1)
                    {
                        spawns.Add(spawn);

                        GameObject trap = Instantiate(Traps[id], room.transform.position + new Vector3(spawn.x, -0.5f, spawn.y),
                            room.transform.rotation);
                        trap.transform.SetParent(transform.Find("Traps"));
                    }
                }
            }
        }
        LoadingBar.Instance.Progress += 1;
    }
}