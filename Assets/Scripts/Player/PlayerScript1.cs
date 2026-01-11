using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript1 : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    public float playerSprint = 3f;

    [Header("Player Health Things")]
    private Health health;

    [Header("Player Animator and Gravity")]
    public CharacterController cC;

    [Header("Player Jump and Velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;

    public Transform playerCamera;

    public float gravity = -9.81f;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    public Animator animator;

    public GameObject playerDamage;

    //public HealthBar healthBar;

    public GameObject endGameMenu;

    public bool isActive = true;


    void Awake()
    {
        health = GetComponent<Health>();
        health.OnDeath += PlayerDie;

    }

    void Start()
    {
        //if (!isActive) return;

        Cursor.lockState = CursorLockMode.Locked;

        //healthBar.GiveFullHealth(playerHealth);
        playerCamera = Camera.main.transform;
        GameManager.Instance.SetPlayer(gameObject);
        health.InitHealth();
    }
    void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);
        PlayerMove();
        Jump();
        Sprint();
    }

    private void PlayerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;
        float speed = Mathf.Clamp01(direction.magnitude);
        animator.SetFloat("Speed", speed);

        //if (direction.magnitude >= 0.1f)
        if(speed >= 0.1f)
        {
            //animator.SetBool("Idle", false);
            //animator.SetBool("Walk", true);
            //animator.SetBool("Running", false);
            //animator.SetBool("RifleWalk", false);
            //animator.SetBool("IdleAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        } else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
        }
    }

    private void Jump()
    {
        if (onSurface && Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        } else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
    }

    private void Sprint()
    {
        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true);
                animator.SetBool("Idle", false);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Running", false);
            }
        }
       
    }

    public void PlayerHitDamage(float takeDamage)
    {
        health.TakeDamage(takeDamage);
        StartCoroutine(PlayerDamage());
    }

    private void PlayerDie()
    {
        GameManager.Instance.GameOver();
        Destroy(gameObject, 1f);
    }

    IEnumerator PlayerDamage ()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(2.18f);
        playerDamage.SetActive(false);
    }
}
