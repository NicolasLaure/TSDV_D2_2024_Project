using System;
using TMPro;
using UnityEngine;

public class EnemiesLeft : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private int _enemiesLeft;

    private void Awake()
    {
        _enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateText();
    }

    public void UpdateCount()
    {
        _enemiesLeft--;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = _enemiesLeft.ToString();
    }
}