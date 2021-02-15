using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController gameController;
    private PlayerInputManager inputManager;
    private List<Player> players;
    private Transform[] startPositions;
    
    private Color[] playerColors = {Color.red, Color.blue, Color.white, Color.yellow};

    void Awake(){
        inputManager = GetComponent<PlayerInputManager>();
        
        players = new List<Player>();
        startPositions = new Transform[]{
            gameController.Player1Start,
            gameController.Player2Start
            };
    }


    void OnPlayerJoined(PlayerInput new_player) {
        int curr_player = players.Count;
        new_player.name = "Player " + (curr_player+1);
        
        Player np = new_player.GetComponent<Player>(); 
        
        players.Add(np);

        Debug.Log("Adding player "+curr_player+" with color: "+ playerColors[curr_player]);
        np.transform.SetPositionAndRotation(startPositions[curr_player].position, startPositions[curr_player].rotation);
        
        gameController.PlayerJoined(np); 
      
    }
}
