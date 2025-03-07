using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform minXObject;
    [SerializeField] private Transform maxXObject;

    private Transform _currentTargetPos;
    private Vector3 _dir;

    private void Start()
    {
        _currentTargetPos = minXObject;
        StartMoving();
    }
    void Update()
    {
        transform.Translate(_dir.normalized * speed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x - _currentTargetPos.position.x) < 0.5f)
            SwitchDir();
    }

    private void SetDir()
    {
        _dir = new Vector3(_currentTargetPos.position.x - transform.position.x, 0, 0);
    }
    private void SwitchDir()
    {
        _currentTargetPos = _currentTargetPos == minXObject ? maxXObject : minXObject;
        SetDir();
    }

    public void Stop()
    {
        _dir = Vector3.zero;
    }
    public void StartMoving()
    {
        SetDir();
    }
}
