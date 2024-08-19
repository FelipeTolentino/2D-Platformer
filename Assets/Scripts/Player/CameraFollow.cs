using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] [Range(0f, 1f)] float followTime = 0.25f;

    [SerializeField] PlayerControls playerCtrl;
    [SerializeField] GroundDetection groundCheck;

    [SerializeField] float offsetJump;

    public Vector3 Offset
    {
        get { return cameraOffset; }
        set { cameraOffset = value; }
    }

    public float CameraOffsetY {
        get { return cameraOffsetY; }
        set { cameraOffsetY = value; }
    }

    public float FollowTime {
        get { return followTime; }
        set { followTime = value; }
    }

    Vector3 velocity;
    float cameraOffsetY;
    private void Awake()
    {
        cameraOffsetY = cameraOffset.y;
    }

    void FixedUpdate()
    {
        Vector3 finalPosition = new Vector3(target.position.x + cameraOffset.x, target.position.y + cameraOffset.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, followTime);
        TurnCamOffsetX();

        if (playerCtrl.IsJumping)
            StartCoroutine(JumpOffset());
    }

    void TurnCamOffsetX()
    {
        if (playerCtrl.HorizontalDirection > 0 && cameraOffset.x < 0 ||
            playerCtrl.HorizontalDirection < 0 && cameraOffset.x > 0)
            cameraOffset.x *= -1;
        else
            return;
    }

    IEnumerator JumpOffset()
    {
        yield return new WaitUntil(() => groundCheck.IsGrounded == false);
        cameraOffset.y = offsetJump;
        yield return new WaitUntil(() => groundCheck.IsGrounded == true);
        cameraOffset.y = cameraOffsetY;
    }
}
