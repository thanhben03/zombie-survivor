using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rifle1 : MonoBehaviour
{
    [Header("Rifle Thing")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;

    [Header("Rifle Effect")]
    public ParticleSystem muzzleSpark;


    public GameObject woodEffect;
    public GameObject goreEffect;

    public float fireCharge = 15f;
    private float nextTimeToShot = 0f;


    [Header("Rifle Ammunition and Shooting")]
    private int maximumAmunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;

    public PlayerScript playerScript;

    public Transform hand;

    public Animator anim;

    public GameObject rifleUI;

    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip gunSound;

    private void Awake()
    {
        transform.SetParent(hand);
        presentAmmunition = maximumAmunition;
        AmmoCount.ammoCount.UpdateAmmoText(maximumAmunition);
        AmmoCount.ammoCount.UpdateMagText(mag);
        rifleUI.SetActive(true);

    }


    private void Update()
    {

        if (presentAmmunition == 0)
        {
            if (!audioSource.isPlaying)
            {

                audioSource.PlayOneShot(reloadSound);
            }
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time > nextTimeToShot)
        {
            anim.SetBool("Fire", true);
            anim.SetBool("Idle", false);
            Debug.Log(anim.GetBool("Fire"));
            nextTimeToShot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("FireWalk", true);
            anim.SetBool("Idle", false);
        }
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("FireWalk", true);
            anim.SetBool("Reloading", false);

        } else
        {
            anim.SetBool("Fire", false);
            //anim.SetBool("Idle", true);
            anim.SetBool("FireWalk", false);

        }
    }

    private void Shoot()
    {
        if (mag == 0)
        {
            return;
        }

        presentAmmunition--;
        audioSource.PlayOneShot(gunSound);
        if (presentAmmunition == 0)
        {
            mag--;
        }

        AmmoCount.ammoCount.UpdateAmmoText(presentAmmunition);
        AmmoCount.ammoCount.UpdateMagText(mag);

        muzzleSpark.Play();
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);
            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();
            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject woodGo = Instantiate(woodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(woodGo, 1f);
            }
            else if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(giveDamageOf);

                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.ZombieHitDamage(giveDamageOf);

                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
        }
    }
    IEnumerator Reload()
    {
        playerScript.playerSpeed = 0f;
        playerScript.playerSprint = 0f;
        setReloading = true;
        anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadingTime);
        anim.SetBool("Reloading", false);
        presentAmmunition = maximumAmunition;

        playerScript.playerSpeed = 1.9f;
        playerScript.playerSprint = 3f;
        setReloading = false;
    }
}
