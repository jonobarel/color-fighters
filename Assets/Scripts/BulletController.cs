using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ColorFightersBase
{
    // Start is called before the first frame update

    // Update is called once per frame

    private float SHOT_COOLDOWN;
    private float BULLET_SPEED; //TODO - move to gameconfig
    public Bullet bulletClass; 
    public CanvasRenderer textBox;
    private bool text_assigned = false;

    void Start() {
        SHOT_COOLDOWN = gameController.config.ShotCooldown;
        BULLET_SPEED = gameController.config.BulletSpeed;
    }

    public void fire(Vector3 position, bool is_left, Player owner) {
        
        
        Bullet new_bullet = Instantiate(bulletClass, position, Quaternion.identity);
        
        new_bullet.owner = owner;
        
        Vector3 dir;
        if (is_left) {
            dir = Vector3.left;
        } else {
            dir = Vector3.right;
        }

        //TODO: replace bullet colour/material with player colour.
        //new_bullet.MyColor = owner.MyColor;

        new_bullet.gameObject.SetActive(true);
        if (!text_assigned) {
            new_bullet.textBox = textBox;
        }
        //Debug.Log("Firing bullet in direction: "+ dir);
        new_bullet.GetComponent<Rigidbody>().velocity = dir * BULLET_SPEED;
        //new_bullet.GetComponent<Rigidbody>().AddForce(dir * BULLET_SPEED);
        Debug.Log("bullet velocity: " + new_bullet.GetComponent<Rigidbody>().velocity);
    }
}
