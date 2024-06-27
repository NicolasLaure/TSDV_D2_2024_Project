using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelHelicopter : MonoBehaviour
{
    [SerializeField] private Transform helicopterSpawnPos;
    [SerializeField] private float travelDuration = 5f;
    [SerializeField] private HelicopterBehaviour helicopter;
    [SerializeField] private GameObject rope;

    private Coroutine callHelicopterCoroutine;
    private Vector3 finalPosition;

    private void Start()
    {
        finalPosition = helicopter.gameObject.transform.position;
        helicopter.gameObject.SetActive(false);
        rope.gameObject.SetActive(false);
    }
    public void CallChopper()
    {
        if (callHelicopterCoroutine != null)
            StopCoroutine(callHelicopterCoroutine);

        callHelicopterCoroutine = StartCoroutine(CallHelicopterCoroutine());
    }

    private IEnumerator CallHelicopterCoroutine()
    {
        float startTime = Time.time;
        float timer = 0;
        helicopter.SetPosition(helicopterSpawnPos.position);
        helicopter.gameObject.SetActive(true);
        while (timer < travelDuration)
        {
            timer = Time.time - startTime;
            helicopter.MoveTowards(helicopterSpawnPos.position, finalPosition, travelDuration, timer);
            yield return null;
        }
        rope.SetActive(true);
    }
}
