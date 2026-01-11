using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickup : MonoBehaviour
{
    public GameObject playerRifle;
    public GameObject pickupRifle;
    public PlayerPunch playerPunch;

    public PlayerScript playerScript;
    private float radius = 2.5f;
    private float nextTimeToPunch = 0f;
    public float punchCharge = 15f;

    public Animator anim;
    public GameObject rifleUI;

    private void Awake()
    {
        playerRifle.SetActive(false);
        rifleUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextTimeToPunch)
        {
            anim.SetBool("Punch", true);


            nextTimeToPunch = Time.time + 1f / punchCharge;
            playerPunch.Punch();
        } else
        {
            anim.SetBool("Punch", false);
        }
        if (Vector3.Distance(transform.position, playerScript.transform.position) < radius)
        {
            playerRifle.SetActive(true);
            pickupRifle.SetActive(false);
            ObjectiveManager.Instance.CompleteObjective(0);

        }
    }
}
