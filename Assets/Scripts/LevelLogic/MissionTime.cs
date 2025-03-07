using UnityEngine;

public class MissionTime : MonoBehaviour
{
    [SerializeField] private bool shouldCount;
    [SerializeField] private LevelResults levelResults;
    [SerializeField] private ClockedTrial clock;
    private float _time = 0;

    private void Start()
    {
        _time = 0;
    }

    void Update()
    {
        if (shouldCount)
        {
            _time += Time.deltaTime;
            clock.OnTimeUpdated(Mathf.FloorToInt(_time));
        }
    }

    public void StartTimer()
    {
        shouldCount = true;
    }

    public void StopTimer()
    {
        shouldCount = false;
        levelResults.SetScores(_time, _time);
    }
}