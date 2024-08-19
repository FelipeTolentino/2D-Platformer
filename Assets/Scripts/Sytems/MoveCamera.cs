using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Vector3 newOffset;
    [SerializeField] [Range(0f, 1f)] float changeTime;

    CameraFollow cameraControl;

    float bkpFollowTime;
    Vector3 bkpOffset;
    float bkpOffsetY;

    public float BkpFollowTime { 
        get { return bkpFollowTime; }
    }

    public Vector3 BkpOffset{
        get { return bkpOffset; }
    }

    public float BkpOffsetY {
        get { return bkpOffsetY; }
    }

    bool done = false;

    private void Start()
    {
        cameraControl = Camera.main.GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !done)
        {
            bkpFollowTime = cameraControl.FollowTime;
            cameraControl.FollowTime = changeTime;
            
            bkpOffset = cameraControl.Offset;
            cameraControl.Offset = newOffset;

            bkpOffsetY = cameraControl.CameraOffsetY;
            cameraControl.CameraOffsetY = newOffset.y;
        }
    }
}
