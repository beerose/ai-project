using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    #region Singleton

    public static LoadingBar Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    public int Progress;
    public int ProgressMax = 11;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = (float) Progress / ProgressMax;
        if (Progress == ProgressMax - 1) Invoke("loaded", 1f);
    }

    private void loaded()
    {
        Progress++;
        slider.value = (float) Progress / ProgressMax;
        GameController.Instance.GameLoaded();
    }
}