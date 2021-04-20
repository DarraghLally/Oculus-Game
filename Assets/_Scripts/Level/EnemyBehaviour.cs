using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    private Animator animator;

    //Patroling
    public Vector3 walkPoint;
    private bool walkPointSet;
    private bool isMoving = false;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Move()
    {
        if(!isMoving)
        {
            // Move at Player
            animator.SetBool("enemyMoving", true);
            isMoving = true; 
            //Debug.Log("Enemy is Moving...");
        }
    }

    private void StopMoving()
    {
        if(isMoving)
        {
            animator.SetBool("enemyMoving", false);
            isMoving = false;
            Debug.Log("Enemy Stopped Moving..."); 
        }
    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            Debug.Log("Walk Point is set...");
            agent.SetDestination(walkPoint);
            this.Move();
        }
            
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            this.StopMoving();
        }
            
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        Debug.Log("Searching for Walk Point...");
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        //Debug.Log("Player: " + player.position); 
        //Debug.Log("Chasing Player....");
        agent.SetDestination(player.position);
        this.Move();
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        this.StopMoving();

        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            // attack animation
            animator.SetBool("enemyAttacking", true);
            //Debug.Log("Enemy is Attacking...");

            // stop attacking
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        //Debug.Log("Enemy Stopping to attack...");
        StopAllCoroutines();
        animator.SetBool("enemyAttacking", false);
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
