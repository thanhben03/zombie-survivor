using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    public float playerSprint = 5f;

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

    public ParticleSystem muzzEffect;
    private bool isSprinting;
    private float jumpForwardSpeed;
    public bool isReloading;


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
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);

        Sprint();      // chỉ check trạng thái
        PlayerMove();  // di chuyển duy nhất tại đây
        Jump();
    }

    private void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        // ===== IDLE =====
        if (direction.magnitude < 0.1f)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Running", false);
            animator.SetBool("Walk", false);
            return;
        }

        // ===== XÁC ĐỊNH TRẠNG THÁI =====
        bool isWalking = !isSprinting && onSurface;
        bool isRunning = isSprinting && onSurface;

        animator.SetBool("Walk", isWalking);
        animator.SetBool("Running", isRunning);

        // ===== TỐC ĐỘ =====
        float speed;
        if (onSurface)
        {
            speed = isRunning ? playerSprint : playerSpeed;
        }
        else
        {
            // đang nhảy → giữ nguyên tốc độ đang có
            speed = jumpForwardSpeed + 1.5f;
        }

        // ===== ANIM SPEED =====
        float animSpeed = isRunning ? 1f : 0.5f;
        animator.SetFloat(
            "Speed",
            Mathf.Lerp(animator.GetFloat("Speed"), animSpeed, Time.deltaTime * 10f)
        );

        // ===== XOAY NHÂN VẬT =====
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            targetAngle,
            ref turnCalmVelocity,
            turnCalmTime
        );

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        // ===== DI CHUYỂN =====
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        cC.Move(moveDirection * speed * Time.deltaTime);
    }




    private void Jump()
    {
        if (onSurface && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");

            jumpForwardSpeed = isSprinting ? playerSprint : playerSpeed;

            velocity.y = Mathf.Sqrt(jumpRange * -2f * gravity);
        }
    }



    private void Sprint()
    {
        if (isReloading)
        {
            isSprinting = false;
            animator.SetBool("Running", false);
            return;
        }
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(2);

        isSprinting = Input.GetKey(KeyCode.LeftShift)
                      && Input.GetKey(KeyCode.W)
                      && onSurface && stateInfo.normalizedTime >= 1f;

        animator.SetBool("Running", isSprinting);
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
