using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float smoothTime = 0.3F;
    public Boundary boundary;

    private Transform target;
    private Vector3 velocity = Vector3.zero;
    private WeaponController weapon;

    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    void Start()
    {
        target = GetComponent<Transform>();
        weapon = GetComponentInChildren<WeaponController>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            target.rotation = Quaternion.Euler(0f, 0f, 0f);
        else if (Input.GetKey(KeyCode.DownArrow))
            target.rotation = Quaternion.Euler(0f, 180f, 0f);
        else if (Input.GetKey(KeyCode.LeftArrow))
            target.rotation = Quaternion.Euler(0f, -90f, 0f);
        else if (Input.GetKey(KeyCode.RightArrow))
            target.rotation = Quaternion.Euler(0f, 90f, 0f);
        else
        {
            if(Input.GetKey(KeyCode.W)) target.rotation = Quaternion.Euler(0f, 0f, 0f);
            else if (Input.GetKey(KeyCode.S))target.rotation = Quaternion.Euler(0f, 180f, 0f);
            else if(Input.GetKey(KeyCode.A))target.rotation = Quaternion.Euler(0f, -90f, 0f);
            else if(Input.GetKey(KeyCode.D))target.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        float xSpeed = 0;
        float ySpeed = 0;
        float zSpeed = 0;
        if (Input.GetKey(KeyCode.W)) zSpeed = playerSpeed;
        if (Input.GetKey(KeyCode.S)) zSpeed = -playerSpeed;
        if (Input.GetKey(KeyCode.A)) xSpeed = -playerSpeed;
        if (Input.GetKey(KeyCode.D)) xSpeed = playerSpeed;
        Vector3 targetPosition = new Vector3(xSpeed, ySpeed, zSpeed) + target.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
        );
    }


    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow)) weapon.Fire();
        if (Input.GetKey(KeyCode.DownArrow)) weapon.Fire();
        if (Input.GetKey(KeyCode.LeftArrow)) weapon.Fire();
        if (Input.GetKey(KeyCode.RightArrow)) weapon.Fire();
    }
}