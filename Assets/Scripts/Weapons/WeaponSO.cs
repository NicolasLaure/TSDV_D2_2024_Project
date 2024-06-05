using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponSO", menuName = "ScriptableObjects/Weapon", order = 0)]
public class WeaponSO : ScriptableObject
{
    [SerializeField] public string weaponName { get { return weaponName; } private set { } }

    [SerializeField] private MagazineSO defaultMagazine;
    [Tooltip("Time in seconds for the weapon to be fired again")]
    [SerializeField] private float shootingCoolDown;
    [SerializeField] private float reloadingDuration;

    [Header("Joystick Rumble variables")]
    [SerializeField] private float rumbleDuration;
    [SerializeField] private float rumbleLowIntensity;
    [SerializeField] private float rumbleHighIntensity;

    private MagazineSO currentMagazine;
    public int MagazineSize { get { return currentMagazine.Size; } }
    public MagazineSO CurrentMagazine { get { return currentMagazine; } set { currentMagazine = value; } }
    public float ShootingCoolDown { get { return shootingCoolDown; } }
    public float ReloadingDuration { get { return reloadingDuration; } }

    public float RumbleDuration { get { return rumbleDuration; } }
    public float RumbleLowIntensity { get { return rumbleLowIntensity; } }
    public float RumbleHighIntensity { get { return rumbleHighIntensity; } }

    public void SetDefault()
    {
        currentMagazine = defaultMagazine;
    }
}
