using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{

    public GameConfig config;
    [SerializeField] private BulletController bulletController;
    [SerializeField] private PlayerController playerController;

    //TODO: figure out how to make this part more dynamic
    public Transform Player1Start;
    public Transform Player2Start;

    public Player PlayerPrefab;
    public void Fire(Player player){
        bulletController.Fire(player);
    }

    public void Awake() {
        if (!(PlayerPrefab && Player1Start && Player2Start)) { //ensure that all these variables are populated
            Debug.Log("Message from " + gameObject.name + "Missing parameters in PlayerPrefab, Player1Start or Player2Start");
            Application.Quit();
        }

        GetComponentInChildren<PlayerInputManager>().playerPrefab = PlayerPrefab.gameObject;
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
