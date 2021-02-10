using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : ColorFightersBase
{
    private Rigidbody rb;
    private SkinnedMeshRenderer smr;
    private float movementX;
    
    //instance constants
    [Header("gameplay constants")]
    private float ACCELERATION;
    private float MAX_SPEED;
    private float JUMP_FORCE;

    //local variables
    [Header("gameplay variables")]
    private bool can_jump = false;
    private bool player_facing_left = false;
    public Animator Anim;
    //public BulletController bulletController;
    //private bool to_fire = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
        Debug.Log("Hero initiated");
        
        ACCELERATION = gameController.config.PlayerAcceleration;
        MAX_SPEED = gameController.config.PlayerMaxSpeed;
        JUMP_FORCE = gameController.config.PlayerJumpForce;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementX > 0.0f) { //TODO: moving right
            player_facing_left = false;
        }
        else if (movementX < 0.0f) { //TODO: moving left
            player_facing_left = true;
        }

        Anim.SetFloat("Blend", movementX);

        Vector3 movement = new Vector3(movementX, 0.0f, 0.0f);
        rb.AddForce(movement * ACCELERATION);

        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -MAX_SPEED, MAX_SPEED), rb.velocity.y, 0.0f);


        
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
            rb.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
            can_jump = false;
        }
    }

    public void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Platform")) {
            can_jump = true;
         }
    }

    public void OnFire(InputValue fireValue) {
        gameController.fire(rb.transform.position, player_facing_left, GetComponent<Player>());
    }

    /*public Color GetColor() {
        return renderer.color;
    }*/

    /*
    private bool is_left() {
        //return transform.rotation.
    }*/
}
