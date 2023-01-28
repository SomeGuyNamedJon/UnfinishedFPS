using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Health and Damage
    public float health = 100f;

    //Pathfinding Variables
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patrol Variables
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    public bool forceStop = false;

    //Attack Variables
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public AttackScript attackScript;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake(){
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        //Check for sight and attack ranges
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange && !forceStop)
            Patrol();

        if(playerInSightRange && !playerInAttackRange  && !forceStop)
            Chase();
        
        if(playerInSightRange && playerInAttackRange)
            Attack();
    }

    //Patrol
    private void Patrol(){

        if(!walkPointSet)
            SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //if walk point reached or agent stopped then find new walk point
        if(distanceToWalkPoint.magnitude < 1f || (agent.velocity.x == 0f  && agent.velocity.x == 0f))
            walkPointSet = false;
    }

    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
            walkPointSet = true;
        }
    }

    //Chase
    private void Chase(){
        agent.SetDestination(player.position);
    }

    //Attack
    private void Attack(){
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){

            attackScript.Attack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    //Health
    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            //play death animation before death
            Die();
        }
    }

    public void Die(){
        GameManager.enemiesSpawned--;
        Debug.Log("Enemies: " + GameManager.enemiesSpawned);
        Destroy(gameObject);
    }

    //Debug Functionality
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }


}
