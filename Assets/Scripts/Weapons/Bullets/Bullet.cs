using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletForce;
    private Rigidbody rb;

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody>();
    }
    void Start()
    {
        rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
    }
}
