using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartSeed : MonoBehaviour
{
    private TextMeshProUGUI seed;

    void Start()
    {
        seed = GetComponent<TextMeshProUGUI>();
        seed.text = BuildOnStart.Instance.Seed.ToString();
    }
}