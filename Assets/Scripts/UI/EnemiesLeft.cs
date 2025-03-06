using System;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesLeft : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private UnityEvent onWin;
    [SerializeField] private VoidEventChannelSO onEnemyDeath;
    private int _enemiesLeft;

    private void Awake()
    {
        _enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateText();
        onEnemyDeath.onVoidEvent += UpdateCount;
    }

    private void OnDisable()
    {
        onEnemyDeath.onVoidEvent -= UpdateCount;
    }

    public void UpdateCount()
    {
        _enemiesLeft--;
        if (_enemiesLeft <= 0)
            onWin?.Invoke();

        UpdateText();
    }

    private void UpdateText()
    {
        text.text = _enemiesLeft.ToString();
    }
}