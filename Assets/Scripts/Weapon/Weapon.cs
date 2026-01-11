using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected WeaponData data;
    protected float nextFireTime;
    protected int currMag;
    protected int currAmmo;
    public virtual void Init(WeaponData weaponData)
    {
        data = weaponData;
        currAmmo = data.maxAmmo;
        currMag = data.mag;
    }

    public abstract void Fire();
}
