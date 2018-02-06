using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpreader : MonoBehaviour
{
	public GameObject[] enemies;
	public GameObject[][] population;
	private EnemyGenerator enemyGenerator;

    void Start()
    {
        GameObject EnemiesFolder = new GameObject();
        EnemiesFolder.transform.SetParent(transform);
        EnemiesFolder.name = "Enemies";
        Invoke("spread", 1.0f);
		enemyGenerator = GetComponent<EnemyGenerator> ();
    }

    private void spread()
    {
		createPopulation ();
        int count = 0;
        foreach (var room in GameObject.FindGameObjectsWithTag("Board"))
        {
            count++;
            if (!GameController.Instace.GetCurrentBoard().name.Equals(room.name))
            {
                Vector3 roof = room.transform.Find("Roof").localScale;
                roof.x -= 0.01f;
                roof.y -= 0.01f;
                roof.z -= 0.01f;
                Vector4 board = new Vector4(-roof.x / 2 + 2, roof.x / 2 - 2, -roof.z / 2 + 2, roof.z / 2 - 2);

                Random.InitState(count);
				int row = Random.Range(0, population.Length);
				Random.InitState(row);
				int col = Random.Range(0, population[row].Length);

				Debug.Log (row + " " + col);
                if (roof.x * roof.z < 100)
                {
					GameObject enemy = Instantiate(population[row][col], room.transform.position, room.transform.rotation);
					Debug.Log (enemy);
                    enemy.transform.SetParent(transform.Find("Enemies"));
                }
                else
                {
                    int iter = Mathf.RoundToInt(roof.x * roof.z / 150);
                    for (int i = 0; i < iter * 2; i++)
                    {
                        Vector2 spawn = new Vector2(Random.Range(board.x, board.y), Random.Range(board.z, board.w));
						GameObject enemy = Instantiate(population[row][col],
                            room.transform.position + new Vector3(spawn.x, 0.0f, spawn.y),
                            room.transform.rotation);
						Debug.Log (enemy);
                        enemy.transform.SetParent(transform.Find("Enemies"));
                    }
                }
            }
        }
        LoadingBar.Instance.Progress += 1;
    }

	private void createPopulation(){
		population = new GameObject[enemies.Length][];
		for (int i = 0; i < enemies.Length; i++) {
			enemyGenerator.start (enemies[i]);
			population[i] = enemyGenerator.getPopulation ();
		}
	}

}