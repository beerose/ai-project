using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSpreader : MonoBehaviour
{
    public GameObject Lantern;

    private List<Vector2> spawns = new List<Vector2>();

    void Start()
    {
        GameObject LanternFolder = new GameObject();
        LanternFolder.transform.SetParent(transform);
        LanternFolder.name = "Lantern";
        Invoke("spread", 0.5f);
    }

    private void spread()
    {
        foreach (var room in GameObject.FindGameObjectsWithTag("Board"))
        {
            spawns = new List<Vector2>();
            Vector3 roof = room.transform.Find("Roof").localScale;
            roof.x -= 0.01f;
            roof.y -= 0.01f;
            roof.z -= 0.01f;
            Vector4 board = new Vector4(-roof.x / 2 + 2, roof.x / 2 - 2, -roof.z / 2 + 2, roof.z / 2 - 2);
            
            int iter = Mathf.RoundToInt(roof.x * roof.z / 40);

            for (int i = 0; i < iter; i++)
            {
                bool restart = false;
                int restartCount = 0;

                Random.InitState(restartCount);
                Vector2 spawn = new Vector2(Random.Range(board.x, board.y), Random.Range(board.z, board.w));
                foreach (var sp in spawns)
                {
                    if (Vector2.Distance(spawn, sp) < 6)
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
                        if (Vector2.Distance(spawn, sp) < 6)
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
                    GameObject lantern = Instantiate(Lantern, room.transform.position + new Vector3(spawn.x, 0.7f, spawn.y), room.transform.rotation);
                    lantern.transform.SetParent(transform.Find("Lantern"));
                }
            }
        }
    }
}