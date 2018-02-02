using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMover : MonoBehaviour
{
    public GameObject Explosion;
    public float LifeTime;
    public float Damage { set; get; }
    public float Speed { set; get; }

    private Rigidbody rb;
    private string ShooterTag;
    private string OtherSameShooterBullet;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
        Destroy(gameObject, LifeTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (ShooterTag == "") Debug.LogWarning("ShooterName is null");
        if (col.tag.Equals("Bullet"))
        {
            if (!col.GetComponent<ShotMover>().GetShooterTag().Equals(ShooterTag))
            {
                Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
        else if (col.tag.Equals("Spell"))
        {
            if (!col.GetComponent<SpellBehaviour>().GetShooterTag().Equals(ShooterTag))
            {
                Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
        else if (!col.tag.Equals(ShooterTag) && !col.tag.Equals("Enemy Eyeshot") && !col.tag.Equals("Item") &&
                 !col.tag.Equals("Floor"))
        {
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
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