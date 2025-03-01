using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float fuseTime;
    [SerializeField] private GameObject bombObject;

    private Coroutine _explosionCoroutine;

    public void Detonate()
    {
        if (_explosionCoroutine == null)
            _explosionCoroutine = StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(fuseTime);
        bombObject.SetActive(true);
    }
}