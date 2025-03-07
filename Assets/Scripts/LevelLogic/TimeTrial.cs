using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeTrial : MonoBehaviour
{
    [SerializeField] private float trialDuration;
    [SerializeField] private TrialTarget target;
    [SerializeField] private List<Transform> possiblePositions = new List<Transform>();
    [SerializeField] private GameSaveSO save;
    [SerializeField] private TrialUI trialUI;
    [SerializeField] private GameObject results;
    private Coroutine _trial;

    private bool _canSpawn = true;
    public Action onTrialFinish;

    private int _highScore = 0;
    private int _score = 0;

    public int HighScore { get { return _highScore; } }
    public int Score { get { return _score; } }

    private void Awake()
    {
        target.onTrialTargetShot += OnTargetShot;
    }

    private void OnEnable()
    {
        OnStartTrial();
        trialUI.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        trialUI.gameObject.SetActive(false);
    }
    private void OnStartTrial()
    {
        if (_trial != null)
            StopCoroutine(_trial);

        _score = 0;
        _trial = StartCoroutine(TrialCoroutine());
    }
    private IEnumerator TrialCoroutine()
    {
        float startTime = Time.time;
        float timer = 0;
        while (timer < trialDuration)
        {
            timer = Time.time - startTime;
            trialUI.OnTimeUpdated((int)trialDuration - (int)timer);
            if (!target.gameObject.activeInHierarchy && _canSpawn)
                StartCoroutine(PresentTarget());
            yield return null;
        }
        if (_score > save.shootingRangeHighScore)
            save.shootingRangeHighScore = _score;

        _highScore = save.shootingRangeHighScore;

        results.GetComponent<LevelResults>().SetScores(_score, _highScore);
        results.SetActive(true);
        onTrialFinish?.Invoke();
    }

    private IEnumerator PresentTarget()
    {
        _canSpawn = false;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));

        target.isEnemy = UnityEngine.Random.value > 0.2f;
        target.transform.position = GetRandomPosition();
        target.gameObject.SetActive(true);
        _canSpawn = true;
    }

    private Vector3 GetRandomPosition()
    {
        Transform randomPos = possiblePositions[UnityEngine.Random.Range(0, possiblePositions.Count)].transform;
        return randomPos.position;
    }

    private void OnTargetShot(bool isEnemy)
    {
        if (isEnemy)
            _score++;
        else
            _score--;

        trialUI.OnScoreUpdated(_score);
    }
}
