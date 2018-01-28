using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSpreader : MonoBehaviour
{
    public GameObject Lantern;

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
            int iter = Mathf.RoundToInt(roof.x * roof.z / 150);

            for (int i = 0; i < iter; i++)
            {
                Vector2 spawn = new Vector2(Random.Range(board.x, board.y), Random.Range(board.z, board.w));
                while (Vector2.Distance(spawn, -spawn) < 3)
                {
                    spawn = new Vector2(Random.Range(board.x, board.y), Random.Range(board.z, board.w));
                }
                Instantiate(Lantern, transform.position + new Vector3(spawn.x, 0.7f, spawn.y), transform.rotation);
                Instantiate(Lantern, transform.position + new Vector3(-spawn.x, 0.7f, -spawn.y), transform.rotation);
            }
        }
    }
}