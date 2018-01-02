using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public class PlayerController : MonoBehaviour
    {
        public float playerSpeed;
        public float smoothTime = 0.3F;

        private Boundary _boundary;
        private Transform target;
        private Vector3 velocity = Vector3.zero;
        private WeaponController weapon;
        private Animator anim;

        public void OnEnterNewRoom()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, 1);
            foreach (var tango in hitColliders)
            {
                if (tango.CompareTag("Floor"))
                {
                    var walls = tango.transform.parent.Find("Walls");
                    _boundary = new Boundary
                    {
                        xMax = walls.Find("West Wall").position.x - .5f,
                        xMin = walls.Find("East Wall").position.x + .5f,
                        zMax = walls.Find("North Wall").position.z - .5f,
                        zMin = walls.Find("South Wall").position.z + .5f,
                    };
                }
            }
        }
        

        void Start()
        {
            OnEnterNewRoom();
            target = GetComponent<Transform>();
            weapon = GetComponentInChildren<WeaponController>();
            anim = GetComponentInChildren<Animator>();
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
                if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
                {
                    if (Input.GetKey(KeyCode.W)) target.rotation = Quaternion.Euler(0f, 0f, 0f);
                    else if (Input.GetKey(KeyCode.S)) target.rotation = Quaternion.Euler(0f, 180f, 0f);
                    else if (Input.GetKey(KeyCode.A)) target.rotation = Quaternion.Euler(0f, -90f, 0f);
                    else if (Input.GetKey(KeyCode.D)) target.rotation = Quaternion.Euler(0f, 90f, 0f);
                }
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
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, _boundary.xMin, _boundary.xMax),
                0.0f,
                Mathf.Clamp(transform.position.z, _boundary.zMin, _boundary.zMax)
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
}
