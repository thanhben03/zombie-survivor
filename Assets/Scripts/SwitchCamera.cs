using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera To Assign")]
    public GameObject aimCam;
    public GameObject aimCanvas;
    public GameObject thirdPersonCam;
    public GameObject thirdPersonCanvas;

    public Animator anim;
    private void Awake()
    {
        aimCam = CameraManager.Instance.aimCam;
        thirdPersonCam = CameraManager.Instance.thirdPersonCam;
        aimCanvas = CameraManager.Instance.aimCanvas;
        thirdPersonCanvas = CameraManager.Instance.thirdPersonCanvas;
        anim = GameManager.Instance.GetCurrentPlayer().GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {

            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("RifleWalk", true);
            anim.SetBool("Walk", true);

            thirdPersonCam.SetActive(false);
            thirdPersonCanvas.SetActive(false);

            aimCam.SetActive(true);
            aimCanvas.SetActive(true);
        }
        else if (Input.GetButton("Fire2"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("RifleWalk", false);
            anim.SetBool("Walk", false);

            thirdPersonCam.SetActive(false);
            thirdPersonCanvas.SetActive(false);

            aimCam.SetActive(true);
            aimCanvas.SetActive(true);
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("IdleAim", false);
            anim.SetBool("RifleWalk", false);

            thirdPersonCam.SetActive(true);
            thirdPersonCanvas.SetActive(true);

            aimCam.SetActive(false);
            aimCanvas.SetActive(false);
        }
    }
}
