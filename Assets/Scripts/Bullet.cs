﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : ColorFightersBase
{
    // Start is called before the first frame update
    public Player owner;
    private Rigidbody rb;

    new public Rigidbody rigidbody {
        get {return rb;}
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {

        Player other_player = other.gameObject.GetComponent<Player>();

        if (other.gameObject.tag == "Player") { //hit a player!
            if (other_player == owner) { return; } //shot ourselves, no friendly fire. Return.
            else { //register a hit
            Debug.Log("Hit: " + owner.name + " --> " + other.gameObject.name);
            }
        } //if "player"
        else if (other.gameObject.tag == "Projectile" && other.GetComponent<Bullet>().owner == owner) {
            //hit our own projectile
            return;
        }
        Destroy(gameObject);
    }
}
