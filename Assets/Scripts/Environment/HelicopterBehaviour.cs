using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBehaviour : MonoBehaviour
{
    [SerializeField] private float rotorsSpeed;

    private AudioSource audioSource;
    private GameObject mainRotor;
    private GameObject tailRotor;
    private void Awake()
    {
        mainRotor = transform.Find("Rotor").gameObject;
        tailRotor = transform.Find("TailRotor").gameObject;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        audioSource.Play();
    }
    private void OnDisable()
    {
        audioSource.Stop();
    }

    private void Update()
    {
        RotateRotors();
    }

    private void RotateRotors()
    {
        mainRotor.transform.rotation *= Quaternion.AngleAxis(rotorsSpeed * Time.deltaTime, Vector3.up);
        tailRotor.transform.rotation *= Quaternion.AngleAxis(rotorsSpeed * Time.deltaTime, Vector3.up);
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
