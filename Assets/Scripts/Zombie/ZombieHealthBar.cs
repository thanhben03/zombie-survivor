using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealthBar : MonoBehaviour
{
    [SerializeField] private Health zombieHealth;
    [SerializeField] private Slider healthSlider;

    void OnEnable()
    {
        zombieHealth.OnInitHealth += GiveFullHealth;
        zombieHealth.OnHealthChanged += UpdateHealth;
    }

    void OnDisable()
    {
        zombieHealth.OnInitHealth -= GiveFullHealth;
        zombieHealth.OnHealthChanged -= UpdateHealth;
    }

    void GiveFullHealth(float curr, float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = curr;
    }

    void UpdateHealth(float current)
    {
        healthSlider.value = current;
    }
}
