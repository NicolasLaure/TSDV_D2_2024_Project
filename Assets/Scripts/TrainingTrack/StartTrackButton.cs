using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrackButton : Target
{
    [SerializeField] private TrainingTrackManager track;
    protected override IEnumerator GotShotCoroutine()
    {
        track.StartTrack();
        yield break;
    }
}
