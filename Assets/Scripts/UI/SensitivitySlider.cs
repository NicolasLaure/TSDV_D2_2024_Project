using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO config;
    private Slider slider;
    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        slider.value = config.lookSensitivity;
    }
}
