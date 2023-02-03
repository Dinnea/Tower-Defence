using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetMaxHealth(float value, bool resetHealth)
    {
        healthSlider.maxValue = value;
        if(resetHealth) healthSlider.value = value; 
    }
    public void SetHealth(float value)
    {
        healthSlider.value = value;
    }

    public void TakeDamage(float value)
    {
        healthSlider.value -= value;
    }

    public void GetHealed(float value)
    {
        healthSlider.value += value;
    }
}
