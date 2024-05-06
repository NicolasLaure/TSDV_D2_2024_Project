using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float shootingForce;
    [SerializeField] private float lifeTime;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rb.AddForce(transform.forward * shootingForce, ForceMode.Impulse);
        Destroy(this.gameObject, lifeTime);
    }
}
