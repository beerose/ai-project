using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float m_HealthModifier = 0f;
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public float DamageFromEnemyCollision = 5;
    public float DelayFromTakeDamage = 0.5f;

    //public float LifeTime;

    private float m_CurrentHealth;
    private bool m_Dead;
    private float lastHit;

    //private Rigidbody rb;
    private PlayerAnimatorController pAnimC;

    private Light playerLight;
    private float startLight;

    public float CurrentHealth
    {
        get { return m_CurrentHealth; }
    }

    private void Awake()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            m_CurrentHealth = m_StartingHealth + m_HealthModifier;
            SetHealthUI();
            SetLight();
        }
    }


    private void Start()
    {
        m_CurrentHealth = m_StartingHealth + m_HealthModifier;
        m_Dead = false;
        pAnimC = GetComponentInChildren<PlayerAnimatorController>();
        playerLight = GetComponentInChildren<Light>();
        startLight = playerLight.range;
        SetHealthUI();
        SetLight();
    }

    public void TakeDamage(float amount)
    {
        if (lastHit + DelayFromTakeDamage < Time.time)
        {
            lastHit = Time.time;
            Debug.Log("Player take damage");
            pAnimC.GetHit();
            m_CurrentHealth -= amount;
            SetHealthUI();
            SetLight();
            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                OnDeath();
            }
        }
    }


    public void SetHealthUI()
    {
        StatisticsUI.Instance.HPupdate(m_CurrentHealth, m_StartingHealth + m_HealthModifier);

        m_Slider.maxValue = m_StartingHealth + m_HealthModifier;
        m_Slider.value = m_CurrentHealth;

        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor,
            m_CurrentHealth / (m_StartingHealth + m_HealthModifier));
    }

    public void UpdateHP()
    {
        if (m_CurrentHealth > m_HealthModifier + m_StartingHealth)
            m_CurrentHealth = m_HealthModifier + m_StartingHealth;
    }

    private void SetLight()
    {
        playerLight.range = m_CurrentHealth / (m_StartingHealth + m_HealthModifier) * startLight;
    }


    private void OnDeath()
    {
        m_Dead = true;
        GameController.Instace.GameOver();
        //rb = GetComponent<Rigidbody>();
        //Destroy(gameObject, LifeTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Bullet"))
        {
            ShotMover shotMover = collider.GetComponent<ShotMover>();
            if (!shotMover.GetShooterTag().Equals("Player") && !m_Dead)
            {
                TakeDamage(shotMover.Damage);
            }
        }
    }

    /*void OnCollisionStay(Collision collision)
    {
        string colTag = collision.transform.tag;
        if ((colTag.Equals("Enemy") || colTag.Equals("Boss")) && lastHit + DelayFromTakeDamage < Time.time && !m_Dead)
        {
            lastHit = Time.time;
            TakeDamage(DamageFromEnemyCollision);
        }
    }*/
}