using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float, float> OnInitHealth;
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    [SerializeField] private float maxHealth = 120f;
    private float currentHealth;

    public bool IsDead => currentHealth <= 0;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {

    }

    public void InitHealth()
    {
        PlayerHealthBar.Instance.SetEvent();
        OnInitHealth?.Invoke(currentHealth, maxHealth);

    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
            OnDeath?.Invoke();
    }

    public float GetHealth()
    {
        return maxHealth;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
