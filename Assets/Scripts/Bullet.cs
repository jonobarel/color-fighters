using System.Collections;
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
        gameController.RegisterHit(other.gameObject, gameObject.GetComponent<Bullet>());
    }

}
