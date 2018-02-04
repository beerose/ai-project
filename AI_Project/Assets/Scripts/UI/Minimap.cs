using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    public Transform player;

    public Shader EffectShader;

    void OnEnable()
    {
        GetComponent<Camera>().SetReplacementShader(EffectShader, "");
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

}
