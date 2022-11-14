using UnityEngine;

public class CHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
    public float ReduceHealth(float delta)
    {
        currentHealth -= delta;
        currentHealth = Mathf.Max(0, currentHealth);
        return currentHealth;
    }
    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    void Awake()
    {
        ResetHealth();
    }
}
