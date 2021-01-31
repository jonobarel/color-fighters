﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController gameController;
    private PlayerInputManager inputManager;
    private List<Player> players;
    private Vector2[] startPositions;
    
    private Color[] playerColors = {Color.red, Color.blue, Color.white, Color.yellow};

    void Awake(){
        inputManager = GetComponent<PlayerInputManager>();
        
        players = new List<Player>();
        startPositions = new Vector2[]{
            gameController.Player1Start.position,
            gameController.Player2Start.position
            };
/*
        for (int i = 0; i < 2; i++) {
            AddPlayer();
        }
*/
    }


//    void AddPlayer() {
    void OnPlayerJoined(PlayerInput new_player) {
        int curr_player = players.Count;
        new_player.name = "Player " + (curr_player+1);
        //Player new_player = inputManager.JoinPlayer().GetComponent<Player>(); 
        
        players.Add(new_player.gameObject.GetComponent<Player>());

        Debug.Log("Adding player "+curr_player+" with color: "+ playerColors[curr_player]);
        
        new_player.transform.position = startPositions[curr_player];
        new_player.GetComponent<ColorFightersBase>().gameController = gameController;
        new_player.GetComponent<ColorFightersBase>().MyColor = playerColors[curr_player];
        new_player.gameObject.SetActive(true);
       
    }
}
