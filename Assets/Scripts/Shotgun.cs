using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public float damage = 25f;
    public float range = 1000f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public float spread = .2f;

    public Camera playerCam;
    public ParticleSystem muzzleFlash;
    public GameObject impact;
    public AudioSource gunshot;

    private float nextTimeToFire = 0f;
    private Vector3 originalAim;
    private Vector3 randomizedAim;

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
        originalAim = playerCam.transform.forward;

        Debug.Log("Y diection shot in: " + originalAim.y);
        Debug.Log("X diection shot in: " + originalAim.x);
        Debug.Log("Z diection shot in: " + originalAim.z);

        RaycastHit hit;
        if(Physics.Raycast(playerCam.transform.position, originalAim, out hit, range)){
            //Debug.Log(hit.transform.name);
            
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if(enemy != null){
                enemy.TakeDamage(damage);
            }

            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        }

        RaycastHit[] randomHit = new RaycastHit[4];
        for(int i = 0; i < 4; i++){
            randomizedAim = originalAim;
            randomizedAim.y += Random.Range(-spread, spread);
            randomizedAim.x += Random.Range(-spread, spread);
            randomizedAim.z += Random.Range(-spread, spread);
            if(Physics.Raycast(playerCam.transform.position, randomizedAim, out randomHit[i], range)){
                
                EnemyAI enemy = randomHit[i].transform.GetComponent<EnemyAI>();
                if(enemy != null){
                    enemy.TakeDamage(damage);
                }

                if(randomHit[i].rigidbody != null){
                    randomHit[i].rigidbody.AddForce(-randomHit[i].normal * impactForce);
                }

                Instantiate(impact, randomHit[i].point, Quaternion.LookRotation(randomHit[i].normal));
            }
        }
    }
}