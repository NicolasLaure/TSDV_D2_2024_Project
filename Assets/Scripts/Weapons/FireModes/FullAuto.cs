using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New FireModeSO", menuName = "ScriptableObjects/Weapons/FireMode/FullAuto", order = 0)]
public class FullAuto : FireMode
{
    public override IEnumerator Fire(Weapon thisWeapon)
    {
        float bulletsShotCount = 0;
        float recoilAmount = 0;
        while (thisWeapon.CanShoot())
        {
            thisWeapon.RecoilCameraDisplacement(recoilAmount);
            thisWeapon.FireWeapon();
            yield return thisWeapon.ShootCoolDown();
            bulletsShotCount++;
            recoilAmount = bulletsShotCount / thisWeapon.WeaponSO.MagazineSize;
        }

        thisWeapon.ResetRecoil();
    }
}