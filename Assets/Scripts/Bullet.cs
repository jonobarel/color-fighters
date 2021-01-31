using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ColorFightersBase
{
    // Start is called before the first frame update
    public Player owner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Player>() == owner) { return; }
        
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>() != owner ) { //register a hit
            Debug.Log("Hit: " + owner.name + " --> " + other.gameObject.name);
        }

        Destroy(gameObject);
    }
}
