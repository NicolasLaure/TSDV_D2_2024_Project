using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelHelicopter : MonoBehaviour
{
    [SerializeField] private Transform helicopterSpawnPos;
    [SerializeField] private float travelDuration = 5f;
    [SerializeField] private HelicopterBehaviour helicopter;
    [SerializeField] private GameObject rope;

    private Coroutine _callHelicopterCoroutine;
    private Vector3 _finalPosition;

    private void Start()
    {
        _finalPosition = helicopter.gameObject.transform.position;
        _callHelicopterCoroutine = null;
        helicopter.gameObject.SetActive(false);
        rope.SetActive(false);
    }
    public void CallChopper()
    {
        if (_callHelicopterCoroutine != null)
            return;

        _callHelicopterCoroutine = StartCoroutine(CallHelicopterCoroutine());
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
            helicopter.MoveTowards(helicopterSpawnPos.position, _finalPosition, travelDuration, timer);
            yield return null;
        }
        rope.SetActive(true);
    }
}
