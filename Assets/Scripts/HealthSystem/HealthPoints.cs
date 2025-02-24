using UnityEngine;
using UnityEngine.Events;

public class HealthPoints : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float maxHealth;
    [SerializeField] private UnityEvent onDeathEvent;
    [SerializeField] private UnityEvent onTakeDamageEvent;

    private bool hasBeenDead = false;

    public float MaxHealth
    {
        get => maxHealth;
    }

    public float CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (hasBeenDead)
            return;

        CurrentHealth -= damage;
        if (IsDead())
        {
            hasBeenDead = true;
            onDeathEvent.Invoke();
        }
        else
        {
            onTakeDamageEvent.Invoke();
        }
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }
}