using System.Collections;
using UnityEngine;

public abstract class FireMode : ScriptableObject
{
    public abstract IEnumerator Fire(Weapon thisWeapon);
}
