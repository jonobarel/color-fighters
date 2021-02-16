using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{

    public GameConfig config;
    public MultipleTargetCamera cam;
    private BulletController bulletController;
    private PlayerController playerController;

    //TODO: figure out how to make this part more dynamic
    public Transform Player1Start;
    public Transform Player2Start;

    public Player PlayerPrefab;
    public void Fire(Player player){
        bulletController.Fire(player);
    }

    
    public void Awake() {
        bulletController = GetComponent<BulletController>();
        playerController = GetComponent<PlayerController>();


        if (!(PlayerPrefab && Player1Start && Player2Start && cam)) { //ensure that all these variables are populated
            Debug.Log("Message from " + gameObject.name + "Missing parameters");
            Application.Quit();
        }

        GetComponentInChildren<PlayerInputManager>().playerPrefab = PlayerPrefab.gameObject;
    }
    public void RegisterHit(GameObject other, Bullet bullet) {
        if (other.CompareTag("Player") && other != bullet.owner) {
            Debug.Log(other.name + " hit");
            PlayerHit(other.GetComponent<Player>());
        }
        else if (!other.CompareTag("Platform")) {
            Debug.Log(other.name + " hit"); //TODO: Solve the bullets hitting player_skin instead of player collider.
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
            Debug.Log(player.name + "is dead!   ");
            //play death animation;
            //handle death logic
        }
    }

    public void PlayerJoined(Player np) {

        np.GetComponent<ColorFightersBase>().gameController = gameObject.GetComponent<GameController>(); //why can't this be accessed directly in new_player? it should be inherited.

        //TODO: replace recolouring with reskinning or applying colour to a material.
        //new_player.GetComponent<ColorFightersBase>().MyColor = playerColors[curr_player]; 

        np.gameObject.SetActive(true);

        cam.Targets.Add(np.transform);
    }
}
