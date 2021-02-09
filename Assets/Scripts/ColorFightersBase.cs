using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(SpriteRenderer))]

public class ColorFightersBase : MonoBehaviour
{
    public GameController gameController;
    private Color myColor;
    public Color MyColor {
        get {return myColor; }
        set {
            Debug.Log("Setting color for "+gameObject.name+": "+myColor);
            myColor = GetComponent<SpriteRenderer>().color = value;
        }
    }

    void Start() {
        if (gameController == null )
            Debug.LogError("Cannot start game without attached GameController in " + gameObject.name);
    }
}
