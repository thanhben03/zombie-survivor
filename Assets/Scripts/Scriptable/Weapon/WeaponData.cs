using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public string weaponType;
    public GameObject weaponPrefab;
    public Vector3 holdOffsetPos;
    public Vector3 holdOffsetRot;
    public AudioClip reloadSound;
    public AudioClip gunSound;
    public float damage;
    public float fireRate;
    public int maxAmmo;
    public int mag;

}

