using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ : MonoBehaviour, IAttackable
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth;

    [SerializeField]public HealthBar healthBar;

    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        SetMaxHP(_maxHealth, true);
    }

    private void Update()
    {
        TakeDmg(0.1f);
    }

    public void SetMaxHP(float hp, bool resetHealth)
    {
        if (hp > 0)
        {
            _maxHealth = hp;
            if (resetHealth) _currentHealth = _maxHealth;
            healthBar.SetMaxHealth(_maxHealth, resetHealth);
        }
        else Debug.Log("Max health must be a positive number.");
    }

    public void SetCurrentHP(float hp)
    {
        _currentHealth = hp;
        healthBar.SetHealth(hp);
    }

    public void TakeDmg(float dmg)
    {
        _currentHealth -= dmg;
        if(_currentHealth< 0) _currentHealth = 0;
        healthBar.SetHealth(_currentHealth);
    }

    public void GetHealed(float heal)
    {
        _currentHealth += heal;
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
        healthBar.SetHealth(_currentHealth);
    }

    public void Die()
    {
        if (_currentHealth <= 0) Debug.Log("HQ has died");
    }
}
