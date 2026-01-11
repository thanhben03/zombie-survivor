using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject thirdPersonCam;
    public GameObject aimCam;
    public GameObject thirdPersonCanvas;
    public GameObject aimCanvas;
    public static CameraManager Instance;

    void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CinemachineVirtualCamera cvcTPC = thirdPersonCam.GetComponent<CinemachineVirtualCamera>();
        CinemachineVirtualCamera cvcAC = aimCam.GetComponent<CinemachineVirtualCamera>();

        cvcTPC.Follow = player.transform;
        cvcTPC.LookAt = player.transform;

        cvcAC.Follow = player.transform;
        cvcAC.LookAt = player.transform;
    }

    public GameObject GetThirdPersonCam()
    {
        return thirdPersonCam;
    }
    public GameObject GetAimCam()
    {
        return aimCam;
    }

    public GameObject GetThirdPersonCanvas()
    {
        return thirdPersonCanvas;
    }

    public GameObject GetAimCanvas()
    {
        return aimCanvas;
    }
}
