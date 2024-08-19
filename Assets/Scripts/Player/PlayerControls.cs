using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float jumpMaxVelocity;
    [SerializeField] float maxVelocity;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;

    //[SerializeField] AudioClip steps;
    //[SerializeField] AudioClip jump;

    Rigidbody2D body;
    GroundDetection groundDetector;
    Animator animator;

    public AudioSource audioSrcPasso;
    [SerializeField]
    AudioSource audioSrcPulo;

    float horizontalDirection;
    float jumpTimeCounter;
    bool isJumping;

    public float HorizontalDirection {
        get { return horizontalDirection; }
    }

    public bool IsJumping { 
        get { return isJumping; }
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        groundDetector = GetComponentInChildren<GroundDetection>();
        animator = GetComponent<Animator>();
        //audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        Move();
            
        Jump();

        Turn();

        
    }

    void Move()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        if (horizontalDirection != 0)
        {
            animator.SetBool("Running", true);

            if (!audioSrcPasso.isPlaying && groundDetector.IsGrounded)
            {
                //audioSrc.clip = steps;
                //audioSrc.loop = true;
                audioSrcPasso.Play();
            }

            body.velocity = new Vector2(horizontalDirection * speed, body.velocity.y);
        }
        else
        {
            animator.SetBool("Running", false);

            audioSrcPasso.Stop();
            //audioSrc.loop = false;

            body.velocity = new Vector2(0, body.velocity.y);
        }
        
    }

    void Jump()
    {
        if (groundDetector.IsGrounded && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");


            //audioSrc.loop = false;
            //audioSrc.clip = jump;
            audioSrcPasso.Stop();
            audioSrcPulo.Play();            

            
            isJumping = true;
            jumpTimeCounter = jumpTime;
            
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
        
        if (Input.GetButton("Jump") && isJumping)
            if (jumpTimeCounter > 0)
            {
                body.velocity = new Vector2(body.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;

        if (Input.GetButtonUp("Jump"))
            isJumping = false;
    }

    void Turn()
    {
        if(horizontalDirection < 0 && transform.localScale.x > 0 ||
           horizontalDirection > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    void SpeedLimit()
    {
        float clampedVelocity;
        if (groundDetector.IsGrounded)
            clampedVelocity = Mathf.Clamp(body.velocity.x, -maxVelocity, maxVelocity);
        else
            clampedVelocity = Mathf.Clamp(body.velocity.x, -jumpMaxVelocity, jumpMaxVelocity);
        body.velocity = new Vector2(clampedVelocity, body.velocity.y);
    }
}
