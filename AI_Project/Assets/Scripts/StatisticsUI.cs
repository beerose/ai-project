using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsUI : MonoBehaviour
{
    #region Singleton

    public static StatisticsUI Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one intence of StatisticsUI found!");
            return;
        }
        Instance = this;
    }

    #endregion

    public Text HP;
    public Text MP;
    public Text DPS;
    public Text Spell;

    public void HPupdate(float t, float tMax)
    {
        HP.text = "HP : " + t.ToString("N0") + " / " + tMax.ToString("N0");
    }

    public void MPUpdate(float t, float tMax)
    {
        MP.text = "MP : " + t.ToString("N0") + " / " + tMax.ToString("N0");
    }

    public void DPSupdate(float t1, float t2)
    {
        DPS.text = "Main DPS / Speed : " + t1.ToString("N2") + " / " + t2.ToString("N0");
    }

    public void SpellUpdate(float t1, float t2)
    {
        Spell.text = "Spell DMG / Cost : " + t1.ToString("N2") + " / " + t2.ToString("N0");
    }
}