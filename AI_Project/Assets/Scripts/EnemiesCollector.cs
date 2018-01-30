using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCollector : MonoBehaviour
{
    public float DelayTime = 0.5f;
    private List<GameObject> enemies = new List<GameObject>();
    private List<string> rooms = new List<string>();

    public void Add(GameObject e, string r)
    {
        enemies.Add(e);
        rooms.Add(r);
        e.SetActive(false);
    }

    public void LoadEnemiesInRoom(string r)
    {
        List <int>toRemove = new List<int>();
        int i = 0;
        foreach (string room in rooms)
        {
            if (room.Equals(r))
            {
                StartCoroutine(activate(enemies[i]));
                toRemove.Add(i);
            }
            i++;
        }
        i = 0;
        foreach (int remove in toRemove)
        {
            enemies.RemoveAt(remove - i);
            rooms.RemoveAt(remove - i);
            i++;
        }
    }

    IEnumerator activate(GameObject e)
    {
        yield return new WaitForSeconds(DelayTime);
        e.SetActive(true);
    }
}