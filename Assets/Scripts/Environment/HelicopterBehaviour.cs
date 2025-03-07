using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBehaviour : MonoBehaviour
{
    [SerializeField] private float rotorsSpeed;

    private AudioSource _audioSource;
    private GameObject _mainRotor;
    private GameObject _tailRotor;
    private void Awake()
    {
        _mainRotor = transform.Find("Rotor").gameObject;
        _tailRotor = transform.Find("TailRotor").gameObject;
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _audioSource.Play();
    }
    private void OnDisable()
    {
        _audioSource.Stop();
    }

    private void Update()
    {
        RotateRotors();
    }

    private void RotateRotors()
    {
        _mainRotor.transform.rotation *= Quaternion.AngleAxis(rotorsSpeed * Time.deltaTime, Vector3.up);
        _tailRotor.transform.rotation *= Quaternion.AngleAxis(rotorsSpeed * Time.deltaTime, Vector3.up);
    }

    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }
    public void MoveTowards(Vector3 startPos, Vector3 destination, float duration, float timer)
    {
        transform.position = Vector3.Lerp(startPos, destination, timer / duration);
    }
}
