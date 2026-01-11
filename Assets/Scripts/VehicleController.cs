using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Wheels colliders")]
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider backLeftWheelCollider;

    [Header("Wheels Transforms")]
    public Transform frontRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform vehicleDoor;

    [Header("Vehicle Engine")]
    public float accelerationForce = 100f;
    public float breakingForce = 200f;
    private float presentBreakForce = 0f;
    private float presentAcceleration = 0f;

    [Header("Wheels Steering")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    [Header("Vehicle Secuirty")]
    public GameObject player;
    private float radius = 5f;
    private bool isOpened = false;

    [Header("Disable Things")]
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;

    [Header("Vehicle Hit Var")]
    public Camera cam;
    public float hitRange = 2f;
    private float giveDamageOf = 200f;
    public GameObject goreEffect;
    private bool isCompletedObj = false;
    private void Start()
    {
        if (GameManager.Instance.GetCurrentPlayer() != null)
        {
            player = GameManager.Instance.GetCurrentPlayer();
        }
        else
        {
            GameManager.Instance.OnPlayerReady += InitPlayer;
        }
    }

    private void InitPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (!isCompletedObj)
            {
                ObjectiveManager.Instance.CompleteObjective(3);
                isCompletedObj = true;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpened = true;
                radius = 5000f;
                //objective complete
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = vehicleDoor.transform.position;
                isOpened = false;
                radius = 5f;
            }
        }

        if (isOpened == true)
        {
            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
            player.SetActive(false);

            MoveVehicle();
            VehicleSteering();
            ApplyBreaks();
            //HitZombies();
        }
        else if (isOpened == false)
        {
            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
            player.SetActive(true);
            //PlayerCharacter.SetActive(true);

        }
    }

    void MoveVehicle()
    {
        //FWD
        frontRightWheelCollider.motorTorque = presentAcceleration;
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;

        presentAcceleration = accelerationForce * -Input.GetAxis("Vertical");
    }

    void VehicleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;

        //animate the wheels
        SteeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(backRightWheelCollider, backRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);
    }

    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);

        WT.position = position;
        WT.rotation = rotation;
    }

    void ApplyBreaks()
    {
        if (Input.GetKey(KeyCode.Space))
            presentBreakForce = breakingForce;
        else
            presentBreakForce = 0f;

        frontRightWheelCollider.brakeTorque = presentBreakForce;
        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
    }

    void HitZombies()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, hitRange))
        {
            Debug.Log(hitInfo.transform.name);

            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(giveDamageOf);
                zombie1.GetComponent<CapsuleCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.ZombieHitDamage(giveDamageOf);
                zombie2.GetComponent<CapsuleCollider>().enabled = false;

                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Zombie1 zombie1 = collision.gameObject.GetComponent<Zombie1>();
        Zombie2 zombie2 = collision.gameObject.GetComponent<Zombie2>();

        if (Mathf.Abs(presentAcceleration) > 10f)
        {
            if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(giveDamageOf);
                zombie1.GetComponent<CapsuleCollider>().enabled = false;

                Instantiate(goreEffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            }
            else if (zombie2 != null)
            {
                zombie2.ZombieHitDamage(giveDamageOf);
                zombie2.GetComponent<CapsuleCollider>().enabled = false;

                Instantiate(goreEffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            }
        }
    }
}
