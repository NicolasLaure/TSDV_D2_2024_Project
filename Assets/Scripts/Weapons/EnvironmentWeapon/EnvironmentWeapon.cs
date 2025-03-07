using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weapon;
    [SerializeField] private float unGrabbableDuration;
    private Rigidbody _rb;
    private Collider _weaponCollider;

    public WeaponSO Weapon { get { return weapon; } }
    private void Awake()
    {
        _weaponCollider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    public IEnumerator ThrowWeapon(Vector3 force)
    {
        _weaponCollider.enabled = false;
        _rb.AddForce(force, ForceMode.Impulse);
        yield return new WaitForSeconds(unGrabbableDuration);
        _weaponCollider.enabled = true;
    }
}
