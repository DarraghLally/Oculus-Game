using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform tr_Player;
    private Animator animator;
    private float f_RotSpeed = 1.5f;
    private bool isMoving = false;
    private bool isAttacking = false;
    private float attackRange = 0.5f;
    private float playerDistance;

    // Use this for initialization
    void Start()
    {
        tr_Player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        //distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        // check distance from player
        playerDistance = CheckRange(tr_Player, this.transform);
        
        // if player is present and outside attack range move to them AND is not attacking
        if(tr_Player && (playerDistance > attackRange))
        {
            this.MoveEnemy();
        }
        else
        {
            this.StopMoving();
        }

        // check if within attack range
        if((attackRange >= this.CheckRange(tr_Player, this.transform)))
        {
            this.AttackPlayer();
        }
        else
        {
            this.StopAttacking();
        } 

    }

    private void MoveEnemy()
    {
        

        if(!isMoving)
        {
            /* Move at Player*/
            animator.SetBool("enemyMoving", true);
            isMoving = true; 
            Debug.Log("Enemy is Moving...");
        }
        else
        {
            /* Look at Player*/
            transform.rotation = Quaternion.Slerp(transform.rotation
                                              , Quaternion.LookRotation(tr_Player.position - transform.position)
                                              , f_RotSpeed * Time.deltaTime);

        }
    }

    private void StopMoving()
    {
        if(isMoving)
        {
            animator.SetBool("enemyMoving", false);
            isMoving = false;
            Debug.Log("Enemy Stopped Moving..."); 
            Debug.Log("Range: " + playerDistance);
        }
    }

    private void AttackPlayer()
    {
        if(!isAttacking)
        {
            animator.SetBool("enemyAttacking", true);
            isAttacking = true;
            Debug.Log("Enemy is Attacking...");
        }

    }

    private void StopAttacking()
    {
        if(isAttacking)
        {
            animator.SetBool("enemyAttacking", false);
            isAttacking = false; 
            Debug.Log("Enemy Stopped Attacking..."); 
        }
    }

    private float CheckRange(Transform object1, Transform object2)
    {
        float distance = Vector3.Distance (object1.position, object2.position);

        return distance;
    }

    IEnumerator AttackDelay(float time)
    {
        Debug.Log("Enemy is Attacking...");
        isAttacking = true;
        yield return new WaitForSeconds(time);
        
        StopAllCoroutines();
        animator.SetBool("enemyAttacking", false);
        isAttacking = false; 
        Debug.Log("Enemy Stopped Attacking...");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collded with: " + other);
    }
}
