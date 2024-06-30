using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float shootingForce;
    [SerializeField] private float lifeTime;
    [SerializeField] private DecalsHandler decals;
    private Rigidbody rb;

    public float ShootingForce { get { return shootingForce; } set { shootingForce = value; } }
    public float LifeTime { get { return lifeTime; } set { lifeTime = value; } }
    public DecalsHandler Decals { get { return decals; } set { decals = value; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rb.AddForce(transform.forward * shootingForce, ForceMode.Impulse);
        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //decals.SpawnBulletHole(collision.transform.position, collision.);
        Destroy(this.gameObject);
    }
}
