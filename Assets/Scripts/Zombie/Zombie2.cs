using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie2 : MonoBehaviour
{
    [Header("Zombie Health & Damage")]
    public float giveDamage;
    public Health health;


    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Camera attackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zombie Guarding Var")]
    public float zombieSpeed;

    [Header("Zombie Attack War")]
    public float timeBtwAttack;
    bool previouslyAttack;

    [Header("Zombie mood/states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvisionRadius;
    public bool playerInattackingRadius;

    [Header("Zombie Animation")]
    public Animator anim;
    public AudioClip zombieDie;
    public AudioClip zombieHurt;

    [Header("Zombie Float Damage Text")]
    public GameObject damagePopupPrefab;
    public Transform popupSpawnPoint;

    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        health.OnDeath += ZombieDie;
        //healthBar.GiveFullHealth(zombieHealth);
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) 
        {
            playerBody = player.transform;
            LookPoint = player.transform;
            // hoặc: player.transform.Find("LookPoint")
        }
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInvisionRadius && !playerInattackingRadius) Idle();
        if (playerInvisionRadius && !playerInattackingRadius)
        {
            Pursueplayer();
        }
        if (playerInattackingRadius && playerInvisionRadius)
        {
            AttackPlayer();
        }
    }

    private void Idle()
    {
        zombieAgent.SetDestination(transform.position);
        anim.SetBool("Idle", true);
        anim.SetBool("Running", false);
    }

    private void Pursueplayer()
    {
        // Ra lệnh cho NavMeshAgent đuổi theo vị trí của người chơi
        if (zombieAgent.SetDestination(playerBody.position))
        {
            // Cập nhật hoạt ảnh khi đang chạy
            anim.SetBool("Idle", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
            //anim.SetBool("Died", false);
        }
        else
        {
            // Hoạt ảnh nếu không thể đuổi theo (ví dụ: Zombie chết hoặc lỗi đường đi)
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);
        }
    }

    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if (!previouslyAttack)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(attackingRaycastArea.transform.position, attackingRaycastArea.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("Attacking" + hitInfo.transform.name);
                PlayerScript playerBody = hitInfo.transform.GetComponent<PlayerScript>();

                if (playerBody != null)
                {
                    playerBody.PlayerHitDamage(giveDamage);
                }
            }

            anim.SetBool("Attacking", true);
            //anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Idle", false);
            //anim.SetBool("Died", false);

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }

    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }

    public void ZombieHitDamage(float takeDamage)
    {
        GameManager.Instance.audioSource.PlayOneShot(zombieHurt);
        health.TakeDamage(takeDamage);
    }

    private void ZombieDie()
    {
        GameManager.Instance.audioSource.PlayOneShot(zombieDie);
        StageManager.Instance.RegisterKill();
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        anim.SetBool("Died", true);
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInattackingRadius = false;
        playerInvisionRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }

}
