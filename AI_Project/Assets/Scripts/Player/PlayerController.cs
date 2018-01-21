using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public class PlayerController : MonoBehaviour
    {
        public float playerSpeed;
        public float smoothTime = 0.3F;
		public float weaponPower = 1;

        private Boundary _boundary;
        private Transform target;
        private Vector3 velocity = Vector3.zero;
        private WeaponController weapon;
        private Animator anim;
        private AudioSource aud;

        public void OnEnterNewRoom()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, 1);
            foreach (var tango in hitColliders)
            {
                if (tango.CompareTag("Floor"))
                {
                    FindObjectOfType<GameController>().ChangeBoard(tango.transform.parent.gameObject);
                    var walls = tango.transform.parent.Find("Walls");
                    _boundary = new Boundary
                    {
                        xMin = Mathf.Ceil(walls.Find("West Wall").position.x),
                        xMax = Mathf.Floor(walls.Find("East Wall").position.x),
                        zMin = Mathf.Ceil(walls.Find("South Wall").position.z),
                        zMax = Mathf.Floor(walls.Find("North Wall").position.z)
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
            aud = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (!GameController.Instace.getGameStatus())
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
                FootStepsSound(xSpeed, zSpeed);
            }
        }


        void LateUpdate()
        {
            if (!GameController.Instace.getGameStatus())
            {
				if (Input.GetKey(KeyCode.UpArrow)) weapon.Fire(weaponPower);
				if (Input.GetKey(KeyCode.DownArrow)) weapon.Fire(weaponPower);
				if (Input.GetKey(KeyCode.LeftArrow)) weapon.Fire(weaponPower);
				if (Input.GetKey(KeyCode.RightArrow)) weapon.Fire(weaponPower);
            }
        }

        void FootStepsSound(float xSpeed, float zSpeed)
        {
            if (xSpeed == 0 && zSpeed == 0) aud.Stop();
            else if (!aud.isPlaying) aud.Play();
        }
    }
}