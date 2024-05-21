using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform minXObject;
    [SerializeField] private Transform maxXObject;

    private Transform currentTargetPos;
    private Vector3 dir;

    private void Start()
    {
        currentTargetPos = minXObject;
        StartMoving();
    }
    void Update()
    {
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x - currentTargetPos.position.x) < 0.5f)
            SwitchDir();
    }

    private void SetDir()
    {
        dir = new Vector3(currentTargetPos.position.x - transform.position.x, 0, 0);
    }
    private void SwitchDir()
    {
        currentTargetPos = currentTargetPos == minXObject ? maxXObject : minXObject;
        SetDir();
    }

    public void Stop()
    {
        dir = Vector3.zero;
    }
    public void StartMoving()
    {
        SetDir();
    }
}
