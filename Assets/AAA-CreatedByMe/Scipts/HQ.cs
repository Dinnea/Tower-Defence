using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    [SerializeField]public HealthBar healthBar;

    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth, true);
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.TakeDamage(1);
    }
}
