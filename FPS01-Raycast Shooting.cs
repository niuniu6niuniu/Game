using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{

    public float range = 100f;   // Shooting range
    public int bulletsPerMag = 30;
    public int bulletsLeft;

    public Transform shootPoint;

    // Shooting delay
    public float fireRate = 0.1f;

    float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1"))
        {
            Fire();   // Execute fire function if press or hold left mouse button
        }
        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime;   // Add into time counter
    }

    private void Fire()
    {
        if (fireTimer < fireRate) return;
        
        RaycastHit hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + "found!");
        }

        fireTimer = 0.0f;   // Reset fire timer
    }
}
