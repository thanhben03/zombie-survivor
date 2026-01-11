using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rifle : Weapon
{
    [Header("Rifle Thing")]
    private Camera cam;
    public float shootingRange = 100f;

    [Header("Rifle Effect")]
    public ParticleSystem muzzleSpark;


    public GameObject woodEffect;
    public GameObject goreEffect;

    private float nextTimeToShot = 0f;


    [Header("Rifle Ammunition and Shooting")]
    private int maximumAmunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadingTime = 2f;
    private bool setReloading = false;

    private PlayerScript playerScript;
    float nextSoundTime = 0f;

    private Animator anim;

    //private AudioSource audioSource;
    //public AudioClip reloadSound;
    //public AudioClip gunSound;

    private void Awake()
    {
        
        cam = Camera.main;
  

    }

    private void Start()
    {
        GameObject player = GameManager.Instance.GetCurrentPlayer();
        anim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerScript>();
        currAmmo = data.maxAmmo;
        //audioSource = player.GetComponent<AudioSource>();
        UIManager.Instance.ShowWeaponUI("Rifle");
        AmmoCount.ammoCount.UpdateAmmoText(data.maxAmmo);
        AmmoCount.ammoCount.UpdateMagText(data.mag);
    }


    private void FixedUpdate()
    {

        Fire();
    }

    private void PlaySound(AudioClip sound)
    {
        GameManager.Instance.audioSource.PlayOneShot(sound);

    }

    //void PlayShootSound()
    //{
    //    if (Time.time < nextSoundTime) return;
    //    PlaySound(data.gunSound);
    //    nextSoundTime = Time.time + data.fireRate;

    //}

    private void Shoot()
    {

        if (muzzleSpark != null)
        {
            muzzleSpark.Stop();
            muzzleSpark.Play();
        }
        else
        {
            Debug.LogError("Bạn chưa gán muzzleSpark vào Rifle script!");
        }

        if (currMag == 0)
        {
            return;
        }

        currAmmo--;
        anim.SetTrigger("Fire");
        if (currAmmo == 0)
        {
            currMag--;
        }
        PlaySound(data.gunSound);
        AmmoCount.ammoCount.UpdateAmmoText(currAmmo);
        AmmoCount.ammoCount.UpdateMagText(currMag);


        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);
            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();
            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(data.damage);
                GameObject woodGo = Instantiate(woodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(woodGo, 1f);
            }
            else if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(data.damage);

                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.ZombieHitDamage(data.damage);

                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
        }
    }
    IEnumerator Reload()
    {
        PlaySound(data.reloadSound);
        setReloading = true;
        playerScript.isReloading = true;

        anim.SetBool("Reloading", true);
        anim.SetBool("Running", false);

        yield return new WaitForSeconds(reloadingTime);

        anim.SetBool("Reloading", false);
        currAmmo = data.maxAmmo;

        playerScript.isReloading = false;

        setReloading = false;
    }


    public override void Fire()
    {
        if (setReloading)
            return;
        if (currAmmo == 0)
        {
            //if (!audioSource.isPlaying)
            //{

            //    audioSource.PlayOneShot(reloadSound);
            //}
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time > nextTimeToShot)
        {

            nextTimeToShot = Time.time + 1f / data.fireRate;
            Shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("Idle", false);
        }
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("Reloading", false);
            muzzleSpark.Play();

        }
        else
        {
            anim.SetBool("Fire", false);
            anim.SetBool("Idle", true);
                
        }
    }
}
