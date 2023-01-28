using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerExplode : AttackScript
{
    public Transform playerPosition;
    public Player player;
    public GameObject playerObj;
    public EnemyAI ai;

    public ParticleSystem explosion;
    public AudioSource boom;
    public MeshRenderer render;
    public CapsuleCollider body;

    public float explosionRange = 10;
    RaycastHit[] inExplosion;
    public float damage = 100f;
    public bool playerTakeDamage = false;

    private void Awake(){
        playerObj = GameObject.Find("Player");
        playerPosition = playerObj.transform;
        player = playerObj.GetComponent<Player>();
    }


    public override void Attack()
    {
        Invoke("Explode", 1f);
    }

    public void Explode(){
        explosion.Play();
        boom.Play();
        render.enabled = false;
        body.enabled = false;
        ai.forceStop = true;

        inExplosion = Physics.SphereCastAll(transform.position, explosionRange, transform.forward);

        foreach (RaycastHit hit in inExplosion)
        {
            Debug.Log(hit.transform.tag);
            if(hit.transform.tag == "Player"){
                playerTakeDamage = true;
            }

            if(hit.transform.tag == "Enemy"){
                Destroy(hit.collider.gameObject);
            }
        }

        if(playerTakeDamage){
            player.TakeDamage(damage);
        }

        Invoke("Die", 10f);
    }

    void Die(){
        ai.Die();
    }
}
