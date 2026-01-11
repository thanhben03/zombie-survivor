using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Health & Damage")]
    public float giveDamage;
    private Health health;
    

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Camera attackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zombie Guarding Var")]
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingpointRadius = 2;

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
    public Transform[] walkPoints;
    public AudioClip zombieDie;


    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        health.OnDeath += ZombieDie;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform wpParent = transform.parent.Find("WalkPoints");

        if (player != null)
        {
            playerBody = player.transform;
            LookPoint = player.transform;
            // hoặc: player.transform.Find("LookPoint")
        }
        if (wpParent != null)
        {
            walkPoints = new Transform[wpParent.childCount];
            for (int i = 0; i < wpParent.childCount; i++)
            {
                walkPoints[i] = wpParent.GetChild(i);
            }
        }
    }

    private void FixedUpdate()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInvisionRadius && !playerInattackingRadius) Guard();
        if (playerInvisionRadius && !playerInattackingRadius)
        {
            Pursueplayer();
        }
        if (playerInattackingRadius && playerInvisionRadius)
        {
            AttackPlayer();
        }
    }

    private void Guard()
    {
        if (walkPoints == null || walkPoints.Length == 0)
            return;

        zombieAgent.speed = zombieSpeed;
        zombieAgent.isStopped = false;

        // Nếu chưa có điểm đến → set
        if (!zombieAgent.hasPath || zombieAgent.remainingDistance <= walkingpointRadius)
        {
            currentZombiePosition = GetNextPoint();
            zombieAgent.SetDestination(walkPoints[currentZombiePosition].position);
        }

        anim.SetBool("Walking", true);
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", false);
    }

    int GetNextPoint()
    {
        if (walkPoints.Length == 1) return 0;

        int next;
        do
        {
            next = UnityEngine.Random.Range(0, walkPoints.Length);
        }
        while (next == currentZombiePosition);

        return next;
    }



    private void Pursueplayer()
    {
        // Ra lệnh cho NavMeshAgent đuổi theo vị trí của người chơi
        if (zombieAgent.SetDestination(playerBody.position))
        {
            // Cập nhật hoạt ảnh khi đang chạy
            anim.SetBool("Walking", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", false);
        }
        //else
        //{
        //    // Hoạt ảnh nếu không thể đuổi theo (ví dụ: Zombie chết hoặc lỗi đường đi)
        //    anim.SetBool("Walking", false);
        //    anim.SetBool("Running", false);
        //    anim.SetBool("Attacking", false);
        //    anim.SetBool("Died", true);
        //}
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
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Died", false);

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }

    private void ActiveAttacking ()
    {
        previouslyAttack = false;
    }

    public void ZombieHitDamage(float takeDamage)
    {
        health.TakeDamage(takeDamage);
    }

    private void ZombieDie()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", false);
        anim.SetBool("Died", true);
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInattackingRadius = false;
        playerInvisionRadius = false;
        UnityEngine.Object.Destroy(gameObject, 5.0f);
    }
}
