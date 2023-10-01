using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongeRotate : MonoBehaviour
{


    [SerializeField]
    private float rotateSpeed;

    private float counter = 0f;

    private void Awake()
    {
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && counter > -90f){
            transform.Rotate(0, 0, - rotateSpeed * Time.deltaTime);
            counter -= rotateSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && counter < 90)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            counter += rotateSpeed * Time.deltaTime;
        }
    }
}
