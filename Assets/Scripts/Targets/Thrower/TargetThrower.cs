using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetThrower : MonoBehaviour
{
    [Range(0, 30)]
    [SerializeField] private float force;
    [SerializeField] private float maxCoolDown;
    [SerializeField] private float horizontalAmplitude;
    [SerializeField] private float verticalAmplitude;
    [SerializeField] private GameObject targetPrefab;

    private bool canThrow = false;
    private void Start()
    {
        StartCoroutine(ThrowCoolDown());
    }
    private void Update()
    {
        if (canThrow)
            ThrowTarget();
    }
    private void ThrowTarget()
    {
        float randomRotationY = Random.Range(-horizontalAmplitude / 2, horizontalAmplitude / 2);
        float randomRotationX = Random.Range(-verticalAmplitude / 2, verticalAmplitude / 2);
        Quaternion throwingRotation = Quaternion.Euler(transform.rotation.eulerAngles.x + randomRotationX, transform.rotation.eulerAngles.y + randomRotationY, transform.rotation.eulerAngles.z);
        Instantiate(targetPrefab, transform.position, throwingRotation);
        StartCoroutine(ThrowCoolDown());
    }

    private IEnumerator ThrowCoolDown()
    {
        canThrow = false;
        yield return new WaitForSeconds(Random.Range(0.5f, maxCoolDown));
        canThrow = true;
    }
}
