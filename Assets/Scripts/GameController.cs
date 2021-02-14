using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameConfig config;
    public BulletController bulletController;
    public PlayerController playerController;

    //TODO: figure out how to make this part more dynamic
    public Transform Player1Start;
    public Transform Player2Start;

    public void Fire(Player player){
        bulletController.Fire(player);
    }

    public void RegisterHit(GameObject other, Bullet bullet) {
        if (other.CompareTag("Player") && other != bullet.owner) {
            Debug.Log(other.name + " hit");
            PlayerHit(other.GetComponent<Player>());
        }
        bulletController.ReturnBullet(bullet);
    }

    public void PlayerHit(Player player) {
        if (player.IsDefending) {
            //TODO: animate a strong defense
            Debug.Log(player.name + " defended!");
            return; //nothing happens
        }
        else {
            Debug.Log(player.name + "is dead!");
            //play death animation;
            //handle death logic
        }
    }
}
