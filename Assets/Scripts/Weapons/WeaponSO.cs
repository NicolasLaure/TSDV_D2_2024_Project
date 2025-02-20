using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponSO", menuName = "ScriptableObjects/Weapons/Weapon", order = 0)]
public class WeaponSO : ScriptableObject
{
    [SerializeField] public string weaponName { get; private set; }

    [SerializeField] private MagazineSO defaultMagazine;
    [Tooltip("Time in seconds for the weapon to be fired again")]
    [SerializeField] private float shootingCoolDown;
    [SerializeField] private float reloadingDuration;

    [Header("Joystick Rumble variables")]
    [SerializeField] private float rumbleDuration;
    [SerializeField] private float rumbleLowIntensity;
    [SerializeField] private float rumbleHighIntensity;

    [Header("Recoil")]
    [Tooltip("Horizontal Displacement is shown on the Y axis of the curve")]
    [SerializeField] private AnimationCurve horizontalDisplacement; 
    [SerializeField] private AnimationCurve verticalDisplacement; 
    
    private MagazineSO currentMagazine;
    public event Action onMagChanged;
    public int MagazineSize { get { return currentMagazine.Size; } }
    public MagazineSO CurrentMagazine
    {
        get { return currentMagazine; }
        set
        {
            onMagChanged?.Invoke();
            currentMagazine = value;
        }
    }
    public float ShootingCoolDown { get { return shootingCoolDown; } }
    public float ReloadingDuration { get { return reloadingDuration; } }

    public float RumbleDuration { get { return rumbleDuration; } }
    public float RumbleLowIntensity { get { return rumbleLowIntensity; } }
    public float RumbleHighIntensity { get { return rumbleHighIntensity; } }

    public AnimationCurve HorizontalDisplacement { get {return horizontalDisplacement; } }
    public AnimationCurve VerticalDisplacement { get {return verticalDisplacement; } }

    public void SetDefault()
    {
        onMagChanged?.Invoke();
        currentMagazine = defaultMagazine;
    }
}
