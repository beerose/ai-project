using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public AnimationClip AttackAnimClip;
    private Animator anim;
    private float fireRate;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        ChangeAnimAttackSpeed();
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow)) anim.SetBool("attack", true);
        else anim.SetBool("attack", false);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            anim.SetBool("walk", true);
        else anim.SetBool("walk", false);
    }

    public void ChangeAnimAttackSpeed()
    {
        fireRate = transform.parent.GetComponentInChildren<WeaponController>().fireRate;
        anim.SetFloat("attackSpeedMultipler", AttackAnimClip.length / fireRate);
    }
}