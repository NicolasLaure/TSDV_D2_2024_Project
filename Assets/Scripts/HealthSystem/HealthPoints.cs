using UnityEngine;
using UnityEngine.Events;

public class HealthPoints : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int maxHealth;
    [SerializeField] private UnityEvent onDeathEvent;
    [SerializeField] public UnityEvent onTakeDamageEvent;

    private bool hasBeenDead = false;
    private bool _canTakeDamage = true;

    public int MaxHealth
    {
        get => maxHealth;
    }

    public int CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (hasBeenDead || !_canTakeDamage)
            return;

        CurrentHealth -= damage;
        if (IsDead())
        {
            hasBeenDead = true;
            onTakeDamageEvent.Invoke();
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

    public void Kill()
    {
        TakeDamage(maxHealth);
    }

    public void ToggleInvincibility()
    {
        _canTakeDamage = !_canTakeDamage;
    }
}