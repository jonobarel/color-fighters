using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : ColorFightersBase
{
    private Rigidbody rb;
    private float movementX;
    public Transform bullet_spawn;

    
    //game constants
    [Header("gameplay constants")]
    [SerializeField] private float ACCELERATION;
    [SerializeField] private float MAX_SPEED;
    [SerializeField] private float JUMP_FORCE;
    [SerializeField] private float GRAVITY_MULTIPLIER;
    [SerializeField] private float SHOT_COOLDOWN;
    private float lastBulletFired;
    
    private static float DEFAULT_TIME = -9999;
    private bool is_defending;
    public bool IsDefending {
        get {return is_defending;}
    }


    //instance variables
    [Header("gameplay variables")]
    private bool can_jump = false;
    private Quaternion rotation;
    public Animator Anim;
    //public BulletController bulletController;
    //private bool to_fire = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Debug.Log("Hero initiated");

        ACCELERATION = gameController.config.PlayerAcceleration;
        MAX_SPEED = gameController.config.PlayerMaxSpeed;
        JUMP_FORCE = gameController.config.PlayerJumpForce;
        GRAVITY_MULTIPLIER = gameController.config.GravityMultiplier;
        SHOT_COOLDOWN = gameController.config.ShotCooldown;

        lastBulletFired = DEFAULT_TIME;

        rotation = transform.rotation;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, 0.0f, 0.0f);
        rb.AddForce(movement * ACCELERATION);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -MAX_SPEED, MAX_SPEED), rb.velocity.y, 0.0f);

        transform.rotation = rotation;
        
    }

    public void OnMove(InputValue movementValue)
    {
        //Debug.Log("Move value: "+ movementValue.Get<Vector2>());
        Vector2 movementVector = movementValue.Get<Vector2>();


        if (movementVector.y < 0) {// crouching!
            movementVector.x = 0; //ignore horizontal input
            is_defending = true;
            PlayerDefend();
            Debug.Log("Defending");
            
        }
        else {
            if (is_defending){
                Debug.Log("Stop defending");
                PlayerStopDefending();
            }
            is_defending = false;
        }

        movementX = movementVector.x;
        //movementY = movementVector.y;

        
        //TODO: replace with logic that animates or rotates the character gradually
        if (movementX * movementX > 0)
        {
            Vector3 new_facing=new Vector3(movementVector.x,0,0).normalized;
            rotation = Quaternion.LookRotation(new_facing, Vector3.up);
        }

    }

    public void OnJump(InputValue jumpValue){
        //Debug.Log("Jump action! " + jumpValue);
        if (can_jump) {
            rb.AddForce(Vector3.up * JUMP_FORCE / GRAVITY_MULTIPLIER, ForceMode.Impulse);
            can_jump = false;
        }
    }

    public void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Platform")) {
            can_jump = true;
            
            //maintain  horizontal velocity when hitting a platform to keep movement smooth
            rb.velocity = new Vector3(-other.relativeVelocity.x,0,0);
         }

    }

    public void OnFire(InputValue fireValue) {
        if (lastBulletFired == DEFAULT_TIME || (Time.time - lastBulletFired) > SHOT_COOLDOWN) {
            gameController.Fire(GetComponent<Player>());
            lastBulletFired = Time.time;
        }
        
    }

    /*public Color GetColor() {
        return renderer.color;
    }*/
    private void PlayerDefend() {
        //TODO: implement this properly
        transform.localScale=new Vector3(1f,0.8f,1f);
    }

    private void PlayerStopDefending() {
        //TODO: implement this properly
        transform.localScale=new Vector3(1f,1f,1f);
    }

}
