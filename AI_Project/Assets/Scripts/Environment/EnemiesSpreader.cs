using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpreader : MonoBehaviour
{
    public GameObject[] Enemies;

    void Start()
    {
        Invoke("active", 1.0f);
    }

    private void active()
    {
        if (!GameController.Instace.GetCurrentBoard().name.Equals(gameObject.name))
        {
            Vector3 roof = transform.Find("Roof").localScale;
            roof.x -= 0.01f;
            roof.y -= 0.01f;
            roof.z -= 0.01f;
            Vector4 board = new Vector4(-roof.x / 2 + 2, roof.x / 2 - 2, -roof.z / 2 + 2, roof.z / 2 - 2);

            System.Random randomNumber = new System.Random(GetInstanceID());
            int id = randomNumber.Next(0, Enemies.Length);

            if (roof.x * roof.z < 100)
                Instantiate(Enemies[id], transform.position, transform.rotation);
            else
            {
                int iter = Mathf.RoundToInt(roof.x * roof.z / 150);

                for (int i = 0; i < iter * 2; i++)
                {
                    Vector2 spawn = new Vector2(Random.Range(board.x, board.y), Random.Range(board.z, board.w));
                    id = randomNumber.Next(0, Enemies.Length);
                    Instantiate(Enemies[id], transform.position + new Vector3(spawn.x, 0.0f, spawn.y),
                        transform.rotation);
                }
            }
        }
    }
}