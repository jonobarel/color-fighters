using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ColorFightersBase
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float movementX;
    
    //instance constants
    [Header("gameplay constants")]
    private float ACCELERATION;
    private float MAX_SPEED;
    private float JUMP_FORCE;

    //local variables
    [Header("gameplay variables")]
    private bool to_jump = false;
    private bool can_jump = false;
    public Animator Anim;
    public BulletController bulletController;
    private bool to_fire = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        Debug.Log("Hero initiated");
        
        ACCELERATION = gameController.config.PlayerAcceleration;
        MAX_SPEED = gameController.config.PlayerMaxSpeed;
        JUMP_FORCE = gameController.config.PlayerJumpForce;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (movementX > 0.0f) {
            sr.flipX = false;
        }
        else if (movementX < 0.0f) {
            sr.flipX = true;
        }
        

        Anim.SetBool("isMoving", (movementX * movementX > 0.1f));

        Vector2 movement = new Vector2(movementX, 0.0f);
        rb.AddForce(movement * ACCELERATION);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MAX_SPEED, MAX_SPEED), rb.velocity.y);

        //Debug.Log("Horizontal velocity: "+rb.velocity.x);
        
        if (to_jump && can_jump) {
            rb.AddForce(Vector2.up * JUMP_FORCE, ForceMode2D.Impulse);
            to_jump = false;
            can_jump = false;
        }
        
        if(to_fire) {
            bulletController.fire(rb.transform.position, sr.flipX);
            to_fire = false;
        }

    }

    public void OnMove(InputValue movementValue)
    {
        //Debug.Log("Move value: "+ movementValue.Get<Vector2>());
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        //movementY = movementVector.y;
    }

    public void OnJump(InputValue jumpValue){
        //Debug.Log("Jump action! " + jumpValue);
        if (can_jump) {
            to_jump = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            //Debug.Log("touched platform, setting can_jump to true");
            can_jump = true;
         }
    }

    public void OnFire(InputValue fireValue) {
        to_fire = true;
    }
}
