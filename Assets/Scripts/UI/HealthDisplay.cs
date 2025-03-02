using System;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private HealthPoints health;

    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        health.onTakeDamageEvent.AddListener(UpdateHealth);
    }

    private void OnDisable()
    {
        health.onTakeDamageEvent.RemoveListener(UpdateHealth);
    }

    public void UpdateHealth()
    {
        float currentHealthPercentage = (float)health.CurrentHealth / health.MaxHealth * 100;
        text.text = Mathf.FloorToInt(currentHealthPercentage).ToString();
        healthBar.value = Mathf.FloorToInt(currentHealthPercentage);
    }
}