using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public void Awake()
    {
        ///                         |
        ///     PlayerController    |
        ///                         |
        // Create a layer mask for the floor
        pc_floorMask = LayerMask.GetMask("Floor");
        // Set up references
        //pc_anim = GetComponent<Animator>();
        pc_rb = GetComponent<Rigidbody>();
        /* ------------------------------------ */

        ///                         |
        ///     PlayerHealth        |
        ///                         |
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<Player>();
        currentHealth = startingHealth;
        /* -------------------------------------------- */

        ///                         |
        ///     PlayerShoot         |
        ///                         |
        //gunAudio.clip = ac;
        //gunAudio.Play();
        //gunAudio.PlayOneShot(,);

        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponentInChildren<ParticleSystem>();
        //shootSound = GetComponent<AudioClip>();
        gunAudio = GetComponent<AudioSource>();
        //gunLight = GetComponentInChildren<Light>();  //GetComponent<Light>();
        gunLine = GetComponentInChildren<LineRenderer>();
        gunAudio.PlayOneShot(shootSound);                                       // DAS MUSS NOCH GETESTET WERDEN!! WENN SOUND NICHT KLAPPEN SOLL DAS HIER AUSKOMMENTIERT WERDEN!!
        /* --------------------------------------------------------------- */
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        ///                         |
        ///     PlayerHealth        |
        ///                         |
        IsDamaged();
        /* ----------------------- */
        ///                         |
        ///     PlayerShoot         |
        ///                         |
        TimeToShootwMouse();
        TimeToShootwController();
        /* ----------------------- */

        ///                         |
        ///     Player Controller   |
        ///                         |
        // Store the input axes
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player
        Move(h, v);
        // Turn the player to face the mouse cursor
        Turning();
        // Animate the player
        Animating(h, v);
        /* ------------------------------------- */
    }



    #region PlayerController
    public float pc_speed = 6f;        // The movementspeed
    Vector3 pc_movement;               // The vector of the direction which gets stored
    //Animator pc_anim;                  // Reference to the animator component
    Rigidbody pc_rb;                   // Reference to the Rigidbody component
    int pc_floorMask;                  // A layer mask of the Floor layer
    float pc_camRayLength = 100f;      // The length of the ray from the camera
    public bool useController;

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input
        pc_movement.Set(h, 0f, v);
        // Normalise the movement
        pc_movement = pc_movement.normalized * pc_speed * Time.deltaTime;
        // Move the player to it's current position plus the movement
        pc_rb.MovePosition(transform.position + pc_movement);
    }

    void Turning()
    {
        #region Turning the Controller on/off
        // Press the BACK Button BLJAAAAAAD!!
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            if (useController == false)
            {
                useController = true;
                Cursor.visible = false;
            }
            else
            {
                useController = false;
                Cursor.visible = true;
            }
        }
        // Press C BLJAAAAAD!!
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (useController == false)
            {
                useController = true;
                Cursor.visible = false;
            }
            else
            {
                useController = false;
                Cursor.visible = true;
            }
        }

        #endregion
        #region Turning with Controller & Mouse
        if (!useController)
        {
            // Create a ray from the mouse cursor on screen
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(camRay.origin, camRay.direction* 10,Color.red, 0.5f);
            // Create a RaycastHit variable to store information
            RaycastHit floorHit;
            // Perform the raycast and if it hits something on the floor layer
            if (Physics.Raycast(camRay, out floorHit, pc_camRayLength, pc_floorMask))
            {
                //Debug.Log("hi");
                // Create a vector from the player to the point on the floor the raycast from the mouse hit
                Vector3 playerToMouse = floorHit.point - transform.position;

                playerToMouse.y = 0f;
                // Create a quaternion(rotation) based on looking
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                // Set the player's rotation to a new rotation
                transform.rotation = newRotation;
            }
        }
        else
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxis("RHorizontal") + Vector3.forward * -Input.GetAxis("RVertical");
            // sqrMagnitude ist fürs lesen da, es guckt ob die Achse Input bekommt
            if (playerDirection.sqrMagnitude > 0.0)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up); ;
            }
        }
        #endregion
    }

    void Animating(float h, float v)
    {
        // Create a boolean
        bool walking = h != 0f || v != 0f;
        // Tell the animator wheter or not the player is walking
        anim.SetBool("IsWalking", walking);
    }
    #endregion

    #region PlayerHealth
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public AudioClip hitClip;
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    Player playerMovement;                            // Reference to the player's movement.

    bool isDead;
    bool damaged;

    // Update is called once per frame


    void IsDamaged()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int _amount)
    {
        damaged = true;
        currentHealth -= _amount;
        healthSlider.value = currentHealth;
        playerAudio.clip = hitClip;
        playerAudio.Play();
        //playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerMovement.enabled = false;
    }
    #endregion

    #region PlayerShooting
    [HideInInspector] public int damagePerShot = 20;              // The damage per each bullet
    [HideInInspector] public float timeBetweenBullets = 2f;     // The time between each bullets
    [HideInInspector] public float range = 100f;                  // The distance of the gun

    float timer;                                // A cooldowntimer
    Ray shootRay;                               // A ray from the gun end forwards
    RaycastHit shootHit;                        // A raycast hit to get info what u hit
    public LayerMask shootableMask;             // a layer mask so u can only hit the shootable layer
    ParticleSystem gunParticles;
    public LineRenderer gunLine; //Brauche ich derzeit nicht(zuständig für den magic stick effekt)
    AudioSource gunAudio;                       // reference to the audio
    //Light gunLight;
    float effectsDisplayTime = 2f;              // The effect Display time

    public AudioClip shootSound;                // AudiClip = mehrere Audios in einem "player"

    public void TimeToShootwController()
    {
        // Add the time per second
        timer += Time.deltaTime;

        // If the Fire1 button is being pressed and there is no cooldown
        if (Input.GetKeyDown(KeyCode.Joystick1Button5) && timer >= timeBetweenBullets)
        {
            //Debug.Log("Zeit zum Schießen bro");
            // "booom" shoot the gun
            Shoot();
            Debug.Log(damagePerShot);
        }
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void TimeToShootwMouse()
    {
        // Add the time per second
        timer += Time.deltaTime;

        // If the Fire1 button is being pressed and there is no cooldown
        if (Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets)
        {
            //Debug.Log("Zeit zum Schießen bro");
            // "booom" shoot the gun
            Shoot();
            Debug.Log(damagePerShot);
        }
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    void Shoot()
    {
        // delay per shoot

        // Reset the timer
        timer = 0f;

        gunAudio.PlayOneShot(shootSound);

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that is starts at the end of the magic-stick and points forward
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobject on the shootable layer and hits something
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // Try and find an EnemyHealth
            EnemyHealth enemyHealth = shootHit.transform.GetComponent<EnemyHealth>();
            // If the EnemyHealth exist
            if (enemyHealth != null)
            {
                //Debug.Log("Enemy Was Found");
                // the enemy takes damage
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            //gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(0, shootRay.origin + shootRay.direction * range);
        }
    }
    void DisableEffects()
    {
        gunLine.enabled = false;
    }
    #endregion
}
