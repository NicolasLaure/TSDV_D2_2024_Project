using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetThrower : MonoBehaviour
{
    [SerializeField] private int targetsQty;
    [Range(0, 300)]
    [SerializeField] private float force;
    [SerializeField] private float maxCoolDown;
    [SerializeField] private float horizontalAmplitude;
    [SerializeField] private float verticalAmplitude;
    [SerializeField] private GameObject targetPrefab;

    public Coroutine throwCoroutine;
    public int TargetsQty { set { targetsQty = value; } }
    private void Start()
    {
        StartCoroutine(StartThrowing());
    }
    private void ThrowTarget()
    {
        float randomRotationY = Random.Range(-horizontalAmplitude / 2, horizontalAmplitude / 2);
        float randomRotationX = Random.Range(-verticalAmplitude / 2, verticalAmplitude / 2);
        Quaternion throwingRotation = Quaternion.Euler(transform.rotation.eulerAngles.x + randomRotationX, transform.rotation.eulerAngles.y + randomRotationY, transform.rotation.eulerAngles.z);
        GameObject target = Instantiate(targetPrefab, transform.position, throwingRotation);
        target.GetComponent<Rigidbody>().AddForce(target.transform.forward * force);
    }
    public IEnumerator StartThrowing()
    {
        for (int i = 0; i < targetsQty; i++)
        {
            yield return ThrowCoolDown();
            ThrowTarget();
        }
    }
    private IEnumerator ThrowCoolDown()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, maxCoolDown));
    }
}
