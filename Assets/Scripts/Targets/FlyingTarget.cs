using System.Collections;
using UnityEngine;

public class FlyingTarget : Target
{
    [SerializeField] private float invulnerableDuration;
    [SerializeField] private float lifeDuration;

    private void Start()
    {
        Destroy(this.gameObject, lifeDuration);
        StartCoroutine(Invulnerability());
    }
    private IEnumerator Invulnerability()
    {
        transform.GetComponentInChildren<Collider>().enabled = false;
        yield return new WaitForSeconds(invulnerableDuration);
        transform.GetComponentInChildren<Collider>().enabled = true;
    }
    protected override IEnumerator GotShotCoroutine()
    {
        Destroy(this.gameObject);
        yield break;
    }
}
