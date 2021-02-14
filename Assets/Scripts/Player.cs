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
    private float ACCELERATION;
    private float MAX_SPEED;
    private float JUMP_FORCE;


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
        if (rb == null) {
            Debug.LogError("Could not find Rigidbody in children objects of "+ gameObject.name);
            Application.Quit();
        }
        Debug.Log("Hero initiated");

        ACCELERATION = gameController.config.PlayerAcceleration;
        MAX_SPEED = gameController.config.PlayerMaxSpeed;
        JUMP_FORCE = gameController.config.PlayerJumpForce;
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
        gameController.fire(GetComponent<Player>());
    }

    /*public Color GetColor() {
        return renderer.color;
    }*/

}
