using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime;

    [SerializeField]
    private GameObject player;

    private void Awake()
    {
        effector = gameObject.GetComponent<PlatformEffector2D>();
    }

    public void changeRotate() {
        effector.rotationalOffset = 180f;
    }

    public void changeBackRotation() {
        effector.rotationalOffset = 0f;
    }

}
