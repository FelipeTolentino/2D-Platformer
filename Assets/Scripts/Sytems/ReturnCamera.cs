using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnCamera : MonoBehaviour
{
    CameraFollow cameraControl;
    MoveCamera moveCamera;

    bool done = false;

    void Start()
    {
        cameraControl = Camera.main.GetComponent<CameraFollow>();
        moveCamera = GetComponentInParent<MoveCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !done)
        {
            cameraControl.FollowTime = moveCamera.BkpFollowTime;
            cameraControl.CameraOffsetY = moveCamera.BkpOffsetY;
            cameraControl.Offset = moveCamera.BkpOffset;
        }
    }

}
