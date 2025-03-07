using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float shootingForce;
    [SerializeField] private float lifeTime;
    [SerializeField] private float timeAfterHit;
    [SerializeField] private DecalsHandler decals;
    private Rigidbody _rb;

    public int damage;

    public float ShootingForce
    {
        get { return shootingForce; }
        set { shootingForce = value; }
    }

    public float LifeTime
    {
        get { return lifeTime; }
        set { lifeTime = value; }
    }

    public DecalsHandler Decals
    {
        get { return decals; }
        set { decals = value; }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rb.AddForce(transform.forward * shootingForce, ForceMode.Impulse);
        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject, timeAfterHit);
    }
}