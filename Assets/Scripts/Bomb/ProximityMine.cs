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

    [SerializeField] private float timeToDestroy;
    private GameObject _target;
    private Coroutine _explosionCoroutine = null;

    public void StartExplosionSequence(GameObject target)
    {
        _target = target;
        if (_explosionCoroutine == null)
            _explosionCoroutine = StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        onFuseStart?.Invoke();
        yield return new WaitForSeconds(fuseDuration);
        onExplosion?.Invoke();
        ApplyDamage();
        OnDestroyCall();
    }

    private void ApplyDamage()
    {
        if (!_target.TryGetComponent<HealthPoints>(out HealthPoints health))
            return;

        int damage = Mathf.FloorToInt(health.MaxHealth * damageOverDistance.Evaluate(Vector3.Distance(transform.position, _target.transform.position) / maxDamageDistance));
        health.TakeDamage(damage);
    }

    public void OnDestroyCall()
    {
        Destroy(this.gameObject, timeToDestroy);
    }
}