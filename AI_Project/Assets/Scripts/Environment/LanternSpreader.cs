using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSpreader : MonoBehaviour
{
    public GameObject Lantern;

    private List<Vector2> spawns = new List<Vector2>();

    void Start()
    {
        Vector3 roof = transform.Find("Roof").localScale;
        roof.x -= 0.01f;
        roof.y -= 0.01f;
        roof.z -= 0.01f;
        Vector4 board = new Vector4(-roof.x / 2 + 2, roof.x / 2 - 2, -roof.z / 2 + 2, roof.z / 2 - 2);

        if (roof.x * roof.z < 80)
            Instantiate(Lantern, transform.position + new Vector3(0.0f, 0.7f, 0.0f), transform.rotation);
        else
        {
            int iter = Mathf.RoundToInt(roof.x * roof.z / 100);

            for (int i = 0; i < iter; i++)
            {
                bool restart = false;
                int restartCount = 0;
                Vector2 spawn = new Vector2(Random.Range(board.x, board.y), Random.Range(board.z, board.w));
                foreach (var sp in spawns)
                {
                    if (Vector2.Distance(spawn, sp) < 6)
                    {
                        restart = true;
                        break;
                    }
                }
                while (Vector2.Distance(spawn, -spawn) < 6 || restart)
                {
                    restart = false;
                    spawn = new Vector2(Random.Range(board.x, board.y), Random.Range(board.z, board.w));
                    foreach (var sp in spawns)
                    {
                        if (Vector2.Distance(spawn, sp) < 6)
                        {
                            restart = true;
                            restartCount++;
                            if (restartCount == 20)
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
                    spawns.Add(-spawn);
                    Instantiate(Lantern, transform.position + new Vector3(spawn.x, 0.7f, spawn.y), transform.rotation);
                    Instantiate(Lantern, transform.position + new Vector3(-spawn.x, 0.7f, -spawn.y),
                        transform.rotation);
                }
            }
        }
    }
}