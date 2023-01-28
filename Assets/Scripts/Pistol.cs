using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float damage = 40f;
    public float range = 1000f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public Camera playerCam;
    public ParticleSystem muzzleFlash;
    public GameObject impact;
    public AudioSource gunshot;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }

    void Shoot(){

        muzzleFlash.Play();
        gunshot.Play();

        RaycastHit hit;
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if(enemy != null){
                enemy.TakeDamage(damage);
            }

            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
