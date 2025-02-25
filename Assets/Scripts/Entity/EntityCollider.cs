using UnityEngine;

public class EntityCollider : MonoBehaviour, IHittable
{
    [Tooltip("Base Value is One, thus 0 means normal damage, 0.5 is 50% more damage and so on")]
    [SerializeField] private float damageMultiplier;
    [SerializeField] private HealthPoints healthPoints;

    private const float BASE_DMG_MULT = 1.0f;

    public void GetHit(int damage)
    {
        int totalDamage = Mathf.FloorToInt((BASE_DMG_MULT + damageMultiplier) * damage);
        healthPoints.TakeDamage(totalDamage);
    }
}