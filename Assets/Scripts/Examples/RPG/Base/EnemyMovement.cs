using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : GradientHealth
{
    public enum AIStates
    {
        Patrol,
        Seek,
        Attack,
        Die
    }
    public AIStates state;
    public Transform player;
    public Transform waypointParent;
    public Transform[] waypoints;
    public int curWaypoint, difficulty;
    public NavMeshAgent agent;
    public float walkSpeed, runSpeed, attackRange, attackSpeed, sightRange, baseDamage, critAmount;
    public bool isDead;
    public float distanceToPoint, changeWaypointWhenThisClose;
    public float stopFromPlayer;
    public float turnSpeed;
    public Animator anim;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //Get waypoints array from waypoint parent
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        //get navMeshAgent from self
        agent = GetComponent<NavMeshAgent>();
        //Set speed of agent
        agent.speed = walkSpeed;
        //Get Animator from self
        anim = GetComponent<Animator>();
        //Set target waypoint;
        curWaypoint = 1;
        //Set Patrol as Default
        Patrol();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);

        Patrol();
        Seek();
        Attack();
        Die();
        FaceTarget();
    }
    void FaceTarget()
    {
        Vector3 turntowardNavSteeringTarget = agent.steeringTarget;
        Vector3 direction = (turntowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    void Patrol()
    {
        if (waypoints.Length <= 0 || Vector3.Distance(player.position, transform.position)  <= sightRange || isDead)
        {
            return;
        }
        agent.stoppingDistance = 0;
        agent.speed = walkSpeed;
        state = AIStates.Patrol;
        anim.SetBool("Walk", true);
        agent.destination = waypoints[curWaypoint].position;
        distanceToPoint = Vector3.Distance(transform.position, waypoints[curWaypoint].position);
        if (distanceToPoint <= changeWaypointWhenThisClose)
        {
            if (curWaypoint < waypoints.Length-1)
            {
                curWaypoint++;
            }
            else
            {
                curWaypoint = 1;
            }
        }
    }
    void Seek()
    {
        if (Vector3.Distance(player.position, transform.position) > sightRange||Vector3.Distance(player.position, transform.position) < attackRange || isDead)
        {
            return;
        }
        state = AIStates.Seek;
        anim.SetBool("Run", true);
        agent.stoppingDistance = stopFromPlayer;
        agent.speed = runSpeed;
        agent.destination = player.position;
    }
    void Attack()
    {
        if (Vector3.Distance(player.position, transform.position) > attackRange || isDead/*|| PlayerHandler.isDead*/)
        {
            return;
        }
        agent.stoppingDistance = stopFromPlayer;
        agent.speed = 0;
        state = AIStates.Attack;
        anim.SetBool("Attack", true);
    }
    void Die()
    {
        if (attributes[0].curValue > 0 || isDead)
        {
            return;
        }
        state = AIStates.Die;
        anim.SetTrigger("Die");
        isDead = true;
        agent.destination = transform.position;
        agent.speed = 0;
        agent.enabled = false;
    }
}