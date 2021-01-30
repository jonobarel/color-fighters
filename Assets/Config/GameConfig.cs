using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorFighters/GameConfig")]
public class GameConfig : ScriptableObject
{
    public float PlayerMaxSpeed;
    public float PlayerJumpSpeed;
    public float PlayerAcceleration;

}
