using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ColorFightersBase
{
    // Start is called before the first frame update

    // Update is called once per frame

    public int MAX_BULLETS_PER_PLAYER = 3; //TODO - move this to gameconfig
    public Bullet bulletClass; 
    public float BulletSpeed; //TODO - move to gameconfig

    public void fire(Vector2 position, bool is_left) {
        //Bullet new_bullet = Instantiate(bulletClass, position, Quaternion.identity);

    }
}
