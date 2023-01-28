using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieBite : AttackScript
{
    public Transform playerPosition;
    public Player player;
    public GameObject playerObj;
    public EnemyAI ai;
    public AudioSource bite;

    public float damage = 30f;

    private void Awake(){
        playerObj = GameObject.Find("Player");
        playerPosition = playerObj.transform;
        player = playerObj.GetComponent<Player>();
    }


    public override void Attack()
    {
        if(ai.playerInAttackRange){
            bite.Play();
            player.TakeDamage(damage);
        }
    }
}
