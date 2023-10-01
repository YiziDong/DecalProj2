using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feet : MonoBehaviour
{

    private GameObject previousLandingFloor;
    public bool Landing;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == previousLandingFloor) {
            Landing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "floor") {
            previousLandingFloor = collision.gameObject;
        }
    }


}
