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
            if (!GameController.Instance.GetCurrentBoard().name.Equals(room.name))
            {
                Random.InitState(count);

                int id = Random.Range(0, Traps.Length);
                int trapSpreadRange;

                if (id == 0) trapSpreadRange = 5;
                else trapSpreadRange = 1;

                Vector3 roof = room.transform.Find("Roof").localScale;
                roof.x -= 0.01f;
                roof.y -= 0.01f;
                roof.z -= 0.01f;
                Vector4 board;
                if (id == 0) board = new Vector4(-roof.x / 2 + 3, roof.x / 2 - 3, -roof.z / 2 + 6, roof.z / 2 - 6);
                else board = new Vector4(-roof.x / 2 + 1, roof.x / 2 - 1, -roof.z / 2 + 1, roof.z / 2 - 1);

                if (id != 0)
                {
                    for (int i = 0; i <= roof.x/2; i++)
                    {
                        spawns.Add(new Vector2(i, 0));
                        spawns.Add(new Vector2(-i, 0));
                    }
                    for (int i = 0; i <= roof.z/2; i++)
                    {
                        spawns.Add(new Vector2(0, i));
                        spawns.Add(new Vector2(0, -i));
                    }
                }


                int iter = Mathf.RoundToInt(roof.x * roof.z / 25);
                if (roof.x < 10 || roof.z < 10) iter = 0;
                for (int i = 0; i < iter; i++)
                {
                    bool restart = false;
                    int restartCount = 0;

                    Random.InitState(restartCount);
                    Vector2 spawn = new Vector2(Random.Range((int) board.x, (int) board.y),
                        Random.Range((int) board.z, (int) board.w));

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
                        spawn = new Vector2(Random.Range((int) board.x, (int) board.y),
                            Random.Range((int) board.z, (int) board.w));
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

                        GameObject trap = Instantiate(Traps[id],
                            room.transform.position + new Vector3(spawn.x, -0.5f, spawn.y),
                            room.transform.rotation);
                        trap.transform.SetParent(transform.Find("Traps"));
                    }
                }
            }
        }
        LoadingBar.Instance.Progress += 1;
    }
}