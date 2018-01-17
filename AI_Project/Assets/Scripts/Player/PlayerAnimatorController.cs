using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public AnimationClip AttackAnimClip;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        ChangeAnimAttackSpeed();
    }

    void Update()
    {
        if (!GameController.Instace.getGameStatus())
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow)) anim.SetBool("attack", true);
            else anim.SetBool("attack", false);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.D))
                anim.SetBool("walk", true);
            else anim.SetBool("walk", false);
        }
        else
        {
            anim.SetBool("attack", false);
            anim.SetBool("walk", false);
            anim.Play("die");
        }
    }

    public void ChangeAnimAttackSpeed()
    {
        WeaponController x = transform.parent.GetComponentInChildren<WeaponController>();
        float newAnimLength = Mathf.Clamp(AttackAnimClip.length / x.fireRate, AttackAnimClip.length,
            5 * AttackAnimClip.length);
        anim.SetFloat("attackSpeedMultipler", newAnimLength);
    }

    public void GetHit()
    {
        anim.Play("GetHit");
    }
}