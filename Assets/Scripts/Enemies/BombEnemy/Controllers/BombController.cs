using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BombController : MonoBehaviour
{
    [SerializeField] private float fuseTime;
    [SerializeField] private GameObject bombObject;

    [SerializeField] private UnityEvent onExplosion;

    private Coroutine _explosionCoroutine;

    public void Detonate()
    {
        if (_explosionCoroutine == null)
            _explosionCoroutine = StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(fuseTime);
        onExplosion?.Invoke();
    }
}