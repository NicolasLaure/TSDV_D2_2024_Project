using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProximityMine : MonoBehaviour
{
    [SerializeField] private float fuseDuration;
    [SerializeField] private float maxDamageDistance;
    [SerializeField] private AnimationCurve damageOverDistance;
    [SerializeField] private UnityEvent onFuseStart;
    [SerializeField] private UnityEvent onExplosion;

    private GameObject _target;
    private Coroutine explosionCoroutine = null;

    public void StartExplosionSequence(GameObject target)
    {
        _target = target;
        if (explosionCoroutine == null)
            explosionCoroutine = StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        onFuseStart?.Invoke();
        yield return new WaitForSeconds(fuseDuration);
        onExplosion?.Invoke();
        ApplyDamage();
    }

    private void ApplyDamage()
    {
        if (!_target.TryGetComponent<HealthPoints>(out HealthPoints health))
            return;

        int damage = Mathf.FloorToInt(health.MaxHealth * damageOverDistance.Evaluate(Vector3.Distance(transform.position, _target.transform.position) / maxDamageDistance));
        health.TakeDamage(damage);
    }
}