using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    public float damage = 35f;
    public float range = 1000f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public Camera playerCam;
    public GameObject muzzleFlash;
    public GameObject impact;
    public AudioSource gunshot;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }

        if(Input.GetButtonUp("Fire1")){
            muzzleFlash.SetActive(false);
        }
    }

    void Shoot(){

        muzzleFlash.SetActive(true);
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
