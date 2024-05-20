using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weapon;
    [SerializeField] private float unGrabbableDuration;
    private Rigidbody rb;
    private Collider weaponCollider;

    public WeaponSO Weapon { get { return weapon; } }
    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public IEnumerator ThrowWeapon(Vector3 force)
    {
        weaponCollider.enabled = false;
        rb.AddForce(force, ForceMode.Impulse);
        yield return new WaitForSeconds(unGrabbableDuration);
        weaponCollider.enabled = true;
    }
}
