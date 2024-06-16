using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FallingTarget : Target
{
    private bool canFall = true;
    [SerializeField] private float fallDuration = 0.3f;
    [SerializeField] private float getUpDuration = 0.3f;

    protected override IEnumerator GotShotCoroutine()
    {
        if (canFall)
        {
            yield return FallCoroutine();
            yield return new WaitForSeconds(recoverDuration);
            yield return GetUpCoroutine();
            //GetUp();
        }
    }
    protected IEnumerator FallCoroutine()
    {
        float startTime = Time.time;
        float timer = 0;
        float targetRotation = transform.rotation.eulerAngles.x + 90;

        if (TryGetComponent<MovingTarget>(out MovingTarget moving))
            moving.Stop();

        while (timer < fallDuration)
        {
            timer = Time.time - startTime;
            transform.rotation = Quaternion.Euler(Mathf.Lerp(transform.rotation.eulerAngles.x, targetRotation, timer / fallDuration), originalRotation.eulerAngles.y, originalRotation.eulerAngles.z);
            yield return null;
        }
    }

    protected IEnumerator GetUpCoroutine()
    {
        float startTime = Time.time;
        float timer = 0;
        // float targetRotation = transform.rotation.x - 90;
        while (timer < getUpDuration)
        {
            timer = Time.time - startTime;
            transform.rotation = Quaternion.Euler(Mathf.Lerp(transform.rotation.eulerAngles.x, originalRotation.eulerAngles.x, timer / getUpDuration), originalRotation.eulerAngles.y, originalRotation.eulerAngles.z);
            yield return null;
        }

        if (TryGetComponent<MovingTarget>(out MovingTarget moving))
            moving.StartMoving();
    }
}
