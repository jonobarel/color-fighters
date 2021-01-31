using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorFighters/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Player")]
    public float PlayerMaxSpeed;
    public float PlayerJumpForce;
    public float PlayerAcceleration;

    [Header("Bullets")]
    public float BulletSpeed;
    public int MaxBulletsPerPlayer;
    
    [Header("General")]
    public int PlayerLives;
    public int MaxPlayers;
}
