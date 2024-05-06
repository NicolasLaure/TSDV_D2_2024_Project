using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New FireModeSO", menuName = "Weapons/FireMode/FullAuto", order = 0)]
public class FullAuto : FireMode
{
    public override IEnumerator Fire(Weapon thisWeapon)
    {
        while (thisWeapon.CanShoot())
        {
            thisWeapon.FireWeapon();
            yield return thisWeapon.ShootCoolDown();
        }
        yield break;
    }
}
