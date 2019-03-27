using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{

    // Shooting
    public float range = 100f;   // Shooting range
    public Transform shootPoint;   // Get Shooting point
    public float fireRate = 0.1f;  // Shooting delay
    float fireTimer;
    private Animator anim;

    // Bullets
    public int bulletsPerMag = 30;   // Bullets in each magzine
    public int bulletsLeft = 200;   // Total bullets left
    public int currentBullets;   // Current bullets in magzaine

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentBullets = bulletsPerMag;
        
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

    void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        // shoot once = clik once
        if (info.IsName("Fire")) anim.SetBool("Fire", false);
    }

    // Fire method
    private void Fire()
    {
        //  Can shoot
        if (fireTimer < fireRate) return;   
        
        RaycastHit hit;

        // Shooting detection
        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + "found!");
        }
        anim.CrossFadeInFixedTime("Fire", 0.01f);//   Play the animation
        //anim.SetBool("Fire", true);
        currentBullets--;   // Minus current bullets by 1
        fireTimer = 0.0f;   // Reset fire timer
    }
}
