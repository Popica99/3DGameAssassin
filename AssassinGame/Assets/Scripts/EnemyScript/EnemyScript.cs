using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    //Patroling
    public Vector2 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("hoodieguy_model").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange) ChasePlayer();
        //if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    //Patroling Functions
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector2 distanceToWalkPoint = (Vector2)transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude<1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomPointRangeX = Random.Range(-walkPointRange, walkPointRange);
        float randomPointRangeY = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector2(transform.position.x + randomPointRangeX, transform.position.y + randomPointRangeY);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    //Chasing Functions
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    //Attacking Functions
    /*private void AttackPlayer()
    {

    }*/
}
