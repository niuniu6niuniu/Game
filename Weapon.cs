using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{

    // Shooting
    public float range = 100f;   // Shooting range
    public Transform shootPoint;   // Get the position of Shooting point 
    
    public float fireRate = 0.1f;   // Shooting delay 
    float fireTimer;    // Shooting time record

    public GameObject hitParticles;   // Shooting particle effect
    public GameObject bulletImpact;   // Shooting bullet hole

    public enum ShootMode { Auto, Semi }   // 2 Shooting modes
    public ShootMode shootMode;
    private bool shootInput;   // Detect shoot input and select shooting mode

    // Animation & Audio
    private Animator _anim;   // Gun shooting & idle animation
    public ParticleSystem muzzleFlash;   // Muzzle Flash, fire animation

    private AudioSource _audioSorce;  // Create audio source
    public AudioClip shootSound;  // AudioClip shootSound


    // Bullets
    public int bulletsPerMag = 30;   // Bullets in each magzine
    public int bulletsLeft = 200;   // Total bullets left
    public int currentBullets;   // Current bullets in magzaine

    // Reloading
    private bool isReloading;

    // ************************ Start is called before the first frame update ************************
    void Start()
    {
        _anim = GetComponent<Animator>();
        _audioSorce = GetComponent<AudioSource>();
        currentBullets = bulletsPerMag;
        
    }

    // ************************ Update is called once per frame ************************
    void Update()
    {
        // Switch shoot mode
        switch (shootMode)
        {
            case ShootMode.Auto:
                shootInput = Input.GetButton("Fire1");
            break;

            case ShootMode.Semi:
                shootInput = Input.GetButtonDown("Fire1");
            break;
        }


        // Left mouse = Fire
        if (shootInput)
        {
            if (currentBullets > 0)
                Fire();   // Execute fire function if press or hold left mouse button
            else if (bulletsLeft > 0)
                DoReload();
        }
        // R = Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentBullets < bulletsPerMag && bulletsLeft > 0)
            DoReload();  
        }

        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime;   // Add into time counter
    }

    // ************************ Fixed Update ************************
    void FixedUpdate()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);

        isReloading = info.IsName("Reload");
        // shoot once = clik once
        // if (info.IsName("Fire")) _anim.SetBool("Fire", false);
    }

    // ************************ Firing ************************
    private void Fire()
    {
        //  Can't shoot: 1.cooling down  2. no bullets 3. is reloading
        if (fireTimer < fireRate || currentBullets <= 0 || isReloading)
            return;   
        
        RaycastHit hit;

        // Shooting detection
        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + "found!");
            // Y axis of particle effect perpendicular with wall
            GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            // Z axis of the bullet hole perpendicular with wall 
            GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

            Destroy(hitParticleEffect, 1f);   // Destroy particle effect after 1 sec
            Destroy(bulletHole, 2f);   // Destroy bullet hole after 2 sec
        }

        _anim.CrossFadeInFixedTime("Fire", 0.01f);   // Play the fire animation (Gun shakes)
        muzzleFlash.Play();   // Play the muzzle flash animation
        PlayShootSound();   // Play shoot sound
        //_anim.SetBool("Fire", true);

        currentBullets--;   // Minus current bullets by 1
        fireTimer = 0.0f;   // Reset fire timer
    }

    // ************************ Reloading ************************
    public void Reload()
    {
        if (bulletsLeft <= 0) return;
        // Number of bullets to Reload
        int bulletsToLoad = bulletsPerMag - currentBullets;
        // If bulletsLeft more than bulletsToLoad, then bulletsToDeduct = bulletsToload else = bulletsLeft
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;   // Deduct the bullets from the bulletsLeft
        currentBullets += bulletsToDeduct;   // Add bulltes to the currentBullets
    }

    // ************************ Play reloading animation ************************
    private void DoReload()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);

        if (isReloading) return;

        _anim.CrossFadeInFixedTime("Reload", 0.01f);
    }


    // ************************ Shoot Sound ************************
    private void PlayShootSound()
    {
        _audioSorce.PlayOneShot(shootSound);
        // _audioSorce.clip = shootSound;
        // _audioSorce.Play();
    }
}
