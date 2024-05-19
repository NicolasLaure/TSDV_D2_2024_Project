using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FallingTarget : Target
{
    private bool canFall = true;
    [SerializeField] private float fallDuration = 0.3f;

    private void GetUp()
    {
        transform.rotation = originalRotation;
        canFall = true;
    }
    protected override IEnumerator GotShotCoroutine()
    {
        if (canFall)
        {
            yield return FallCoroutine();
            yield return new WaitForSeconds(recoverDuration);
            GetUp();
        }
    }
    private IEnumerator FallCoroutine()
    {
        float startTime = Time.time;
        float timer = 0;
        float targetRotation = transform.rotation.x + 90;
        while (timer < fallDuration)
        {
            timer = Time.time - startTime;
            transform.rotation = Quaternion.Euler(Mathf.Lerp(transform.rotation.x, targetRotation, timer / fallDuration), 0, 0);
            yield return null;
        }
    }
}
