using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterbugDetectiveZone : MonoBehaviour
{

    public bool inZone = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "player") {
            inZone = true;
        }
    }
    
}
