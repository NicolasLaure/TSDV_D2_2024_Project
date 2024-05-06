using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New FireModeSO", menuName = "Weapons/FireMode/Burst", order = 0)]
public class Burst : FireMode
{
    [Header("Burst")]
    [SerializeField] private int bulletsPerBurst;
    [SerializeField] private float burstDuration;
    [SerializeField] private bool isBurstAuto;
    public override IEnumerator Fire(Weapon thisWeapon)
    {
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            thisWeapon.FireWeapon();
            if (!thisWeapon.HasBullets())
                break;
            yield return new WaitForSeconds(burstDuration / bulletsPerBurst);
        }
    }
}
