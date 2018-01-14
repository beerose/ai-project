using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofHideScript : MonoBehaviour
{
    private Material mat;
    private Color col;
    private float t;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        col = mat.color;
    }
    void LateUpdate()
    {
        if (!Application.isEditor)
        {
            if (GameController.Instace.GetCurrentBoard().transform != transform.parent)
            {
                t += 2 * Time.deltaTime;
                col.a = Mathf.Clamp(t,0,1);
                mat.color = col;
            }
            else
            {
                t = 0;
                col.a = 0;
                mat.color = col;
            }
        }
    }
}