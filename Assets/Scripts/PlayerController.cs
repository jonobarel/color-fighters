using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController gameController;
    private PlayerInputManager inputManager;
    private Player player1;
    private Player player2;
    void Awake(){
        inputManager = GetComponent<PlayerInputManager>();

        player1 = inputManager.JoinPlayer().GetComponent<Player>();
        player2 = (Player)inputManager.JoinPlayer().GetComponent<Player>();

        player1.GetComponent<Transform>().position = gameController.Player1Start.position;
        player2.GetComponent<Transform>().position = gameController.Player2Start.position;
        
        player1.gameController = gameController;
        player2.gameController = gameController;

        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(true);
        
    }
  
}
