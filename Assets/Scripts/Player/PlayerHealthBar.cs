using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider healthSlider;
    public static PlayerHealthBar Instance;


    void Awake()
    {
        Instance = this;
    }
    public void SetEvent()
    {
        playerHealth = GameManager.Instance.GetCurrentPlayer().GetComponent<Health>();
        Debug.Log("SET EVENT");
        if (playerHealth == null)
        {
            Debug.Log("Player Health: NULL");
        }
        playerHealth.OnHealthChanged += UpdateHealth;
        playerHealth.OnInitHealth += GiveFullHealth;
    }

    void OnDisable()
    {
        playerHealth.OnInitHealth -= GiveFullHealth;
        playerHealth.OnHealthChanged -= UpdateHealth;
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
