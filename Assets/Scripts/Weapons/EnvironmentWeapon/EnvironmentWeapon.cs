using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weapon;
    private Rigidbody rb;

    public WeaponSO Weapon { get { return weapon; } }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public IEnumerator ThrowWeapon()
    {
        yield return null;
    }
}
