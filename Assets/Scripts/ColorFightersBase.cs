using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFightersBase : MonoBehaviour
{
    public GameController gameController;

    void Start() {
        if (gameController == null )
            Debug.LogError("Cannot start game without attached GameController in " + gameObject.name);
    }
}
