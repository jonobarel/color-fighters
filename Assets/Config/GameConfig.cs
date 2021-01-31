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
    public float GravityMultiplier;
    public int PlayerLives;

    [Header("Bullets")]
    public float BulletSpeed;
    public float ShotCooldown;
    
    [Header("Powerup")]
    public float PowerupShotSpeedMultiplier;
    public float PowerupSizeMultiplier;

    [Header("World")]
    public float PowerupSpawnInterval;
    
}
