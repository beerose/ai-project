using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour
{
    private string ShooterTag;
    public float Damage;
    public float ManaCost;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
    
    public void SetShooterTag(string s)
    {
        ShooterTag = s;
    }

    public string GetShooterTag()
    {
        return ShooterTag;
    }
}