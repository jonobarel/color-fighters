using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameConfig config;
    public BulletController bulletController;
    public PlayerController playerController;

    public Transform Player1Start;
    public Transform Player2Start;

    public void fire(Vector2 pos, bool is_left){
        bulletController.fire(pos, is_left);
    }
}
