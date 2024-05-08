using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New FireModeSO", menuName = "Weapons/FireMode/SemiAuto", order = 0)]
public class SemiAuto : FireMode
{
    public override IEnumerator Fire(Weapon thisWeapon)
    {
        thisWeapon.FireWeapon();
        yield break;
    }
}
