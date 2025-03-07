using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO config;
    private Slider _slider;
    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _slider.value = config.lookSensitivity;
    }
}
