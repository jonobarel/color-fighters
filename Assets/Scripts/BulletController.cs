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

    void Start() {
        SHOT_COOLDOWN = gameController.config.ShotCooldown;
        BULLET_SPEED = gameController.config.BulletSpeed;
    }

    public void fire(Vector2 position, bool is_left, Player owner) {
        
        
        Bullet new_bullet = Instantiate(bulletClass, position, Quaternion.identity);
        
        new_bullet.owner = owner;
        
        Vector2 dir;
        if (is_left) {
            dir = Vector2.left;
        } else {
            dir = Vector2.right;
        }

        new_bullet.MyColor = owner.MyColor;

        new_bullet.gameObject.SetActive(true);
        
        //Debug.Log("Firing bullet in direction: "+ dir);
        new_bullet.GetComponent<Rigidbody2D>().velocity = dir * BULLET_SPEED;
        
    }
}
