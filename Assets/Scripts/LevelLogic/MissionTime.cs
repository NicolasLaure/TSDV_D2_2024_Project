using UnityEngine;

public class MissionTime : MonoBehaviour
{
    [SerializeField] private bool shouldCount;
    [SerializeField] private LevelResults levelResults;
    [SerializeField] private ClockedTrial clock;
    private float time = 0;

    private void Start()
    {
        time = 0;
    }

    void Update()
    {
        if (shouldCount)
        {
            time += Time.deltaTime;
            clock.OnTimeUpdated(Mathf.FloorToInt(time));
        }
    }

    public void StartTimer()
    {
        shouldCount = true;
    }

    public void StopTimer()
    {
        shouldCount = false;
        levelResults.SetScores(time, time);
    }
}