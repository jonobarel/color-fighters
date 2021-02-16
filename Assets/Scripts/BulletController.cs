using System.Collections;
using UnityEngine;

public class BulletController : ColorFightersBase
{
    // Start is called before the first frame update

    // Update is called once per frame

    private float SHOT_COOLDOWN;
    private Stack bulletStack;
    private int bulletCounter = 0;

    [SerializeField] private float BULLET_SPEED; //TODO - move to gameconfig
    public Bullet bulletClass; 


    void Start() {
        if (!bulletClass) {
            Debug.LogError("missing bulletClass prefab");
            Application.Quit(-1);
        }

        SHOT_COOLDOWN = gameController.config.ShotCooldown;
        BULLET_SPEED = gameController.config.BulletSpeed;
        bulletStack = new Stack(8);
    }

    public void Fire(Player owner) {
        
        Bullet new_bullet;

        if (bulletStack.Count == 0) {//no bullets available
            new_bullet = Instantiate(bulletClass);
            new_bullet.name = "Bullet "+bulletCounter++;
            new_bullet.gameController = gameController;
        }
        else {
            new_bullet = (Bullet)bulletStack.Pop();
        }

        new_bullet.transform.position = owner.bullet_spawn.transform.position;
        new_bullet.transform.rotation = Quaternion.identity;
        new_bullet.owner = owner;

        //TODO: replace bullet colour/material with player colour.
        //new_bullet.MyColor = owner.MyColor;

        Vector3 firing_dir = owner.transform.forward;
        firing_dir.y = 0; //fixes some minor wobbling for now TODO: remove this 

        Debug.Log("Spawning a bullet at: " + new_bullet.transform.position + " with firing direction: " + firing_dir);

        new_bullet.gameObject.SetActive(true);
        new_bullet.GetComponent<Rigidbody>().AddForce(firing_dir * BULLET_SPEED, ForceMode.VelocityChange);
    }

    public void ReturnBullet(Bullet bullet) {
        bullet.rigidbody.velocity = Vector3.zero;
        bullet.gameObject.SetActive(false);
        bulletStack.Push(bullet);
    }
}
