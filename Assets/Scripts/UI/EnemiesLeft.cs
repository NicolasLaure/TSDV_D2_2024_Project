using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesLeft : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private UnityEvent onWin;
    private int _enemiesLeft;

    private void Awake()
    {
        _enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateText();
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