using UnityEngine;
using UnityEngine.Events;

public class BombCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HealthPoints>(out HealthPoints healthPoints))
        {
            healthPoints.TakeDamage(healthPoints.MaxHealth);
        }
    }
}