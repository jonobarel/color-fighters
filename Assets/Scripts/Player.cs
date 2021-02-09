using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : ColorFightersBase
{
    private Rigidbody rb;
    private SpriteRenderer sr;
    private float movementX;
    
    //instance constants
    [Header("gameplay constants")]
    private float ACCELERATION;
    private float MAX_SPEED;
    private float JUMP_FORCE;

    //local variables
    [Header("gameplay variables")]
    //private bool to_jump = false;
    private bool can_jump = false;
    public Animator Anim;
    //public BulletController bulletController;
    //private bool to_fire = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        Debug.Log("Hero initiated");
        
        ACCELERATION = gameController.config.PlayerAcceleration;
        MAX_SPEED = gameController.config.PlayerMaxSpeed;
        JUMP_FORCE = gameController.config.PlayerJumpForce;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementX > 0.0f) {
            //sr.flipX = false;
        }
        else if (movementX < 0.0f) {
            //sr.flipX = true;
        }
        

        //Anim.SetBool("isMoving", (movementX * movementX > 0.1f));
        Anim.SetFloat("Blend", movementX);

        Vector3 movement = new Vector3(movementX, 0.0f, 0.0f);
        rb.AddForce(movement * ACCELERATION);

        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -MAX_SPEED, MAX_SPEED), rb.velocity.y, 0.0f);

        //Debug.Log("Horizontal velocity: "+rb.velocity.x);
        /*
        if (to_jump && can_jump) {
            
            to_jump = false;
            can_jump = false;
        }*/
        
    }

    public void OnMove(InputValue movementValue)
    {
        Debug.Log("Move value: "+ movementValue.Get<Vector2>());
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        //movementY = movementVector.y;
    }

    public void OnJump(InputValue jumpValue){
        //Debug.Log("Jump action! " + jumpValue);
        if (can_jump) {
            //to_jump = true;
            rb.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
            can_jump = false;
        }
    }

    public void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Platform")) {
            //Debug.Log("touched platform, setting can_jump to true");
            can_jump = true;
         }
    }

    public void OnFire(InputValue fireValue) {
        //gameController.fire(rb.transform.position, sr.flipX, GetComponent<Player>());
    }

    public Color GetColor() {
        return sr.color;
    }
}
