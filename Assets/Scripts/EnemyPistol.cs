using UnityEngine;

public class EnemyPistol : AttackScript
{
    public float damage = 40f;
    public float range = 1000f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public GameObject enemy;
    public ParticleSystem muzzleFlash;
    public AudioSource gunshot;

    public Player player;
    public GameObject playerObj;

    private float nextTimeToFire = 0f;

    public void Awake(){
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    public override void Attack(){

        muzzleFlash.Play();
        gunshot.Play();

        RaycastHit hit;
        if(Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, range)){
            //Debug.Log(hit.collider.tag);

            if(hit.collider.tag == "Player"){
                //Debug.Log("Take Damage");
                player.TakeDamage(damage);
            }

            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}