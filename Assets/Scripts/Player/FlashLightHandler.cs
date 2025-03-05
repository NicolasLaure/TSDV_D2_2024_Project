using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightHandler : MonoBehaviour
{
    [SerializeField] private GameObject lightObject;

    public void ToggleLight()
    {
        lightObject.SetActive(!lightObject.activeInHierarchy);
    }
}