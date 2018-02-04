using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewSeed : MonoBehaviour
{
    private TMP_InputField seed;

    void Start()
    {
        seed = GetComponent<TMP_InputField>();
    }

    public void SeedChange()
    {
        if (seed.text.Equals("")) GameController.Instance.Seed = BuildOnStart.Instance.Seed;
        else GameController.Instance.Seed = int.Parse(seed.text);
    }
}