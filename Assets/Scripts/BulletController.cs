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

    public void fire(Player owner) {
        
        
        Bullet new_bullet = Instantiate(bulletClass, owner.bullet_spawn.transform.position ,Quaternion.identity);
        
        new_bullet.owner = owner;

        //TODO: replace bullet colour/material with player colour.
        //new_bullet.MyColor = owner.MyColor;

        new_bullet.gameObject.SetActive(true);

        Vector3 firing_dir = owner.transform.forward;

        Debug.Log("Spawning a bullet at: " + new_bullet.transform.position + " with firing direction: " + firing_dir);

        new_bullet.GetComponent<Rigidbody>().AddForce( firing_dir * BULLET_SPEED);
    }
}
