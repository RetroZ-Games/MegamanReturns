using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Jumping
    private bool isGrounded;
    public Transform feetPos;
    public Transform frontPos;
    public Vector2 checkVector;
    public Vector2 checkVectorWall;
    public float jumpForce = 5f;
    public float slideForce = 3f;

    private float jumpTimeCounter;
    public float jumpTime = 2f;
    private bool isJumping;

    // Movement
    public float movement_speed = 3f;
    private Vector2 movement;
    private float horizontalAxis;
    public LayerMask whatIsGround;

    // Limitacion velocidad vertical
    public float maxVerticalSpeed = 15f;
    public float maxVerticalSpeedSliding = 2f;

    // Physics
    private Rigidbody2D player_physics;

    //sprite & animations
    private bool facingRight = true;
    private bool isOnWall = false;
    private bool isSliding = false;
    private bool slideFlip = false;
    private Animator animator;

    bool wallJumping = false;
    public float xWallForce;
    public float yWallForce;
    public float walljumpTime;

    // Start is called before the first frame update
    void Start()
    {
        player_physics = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapBox(feetPos.position, checkVector, 0f, whatIsGround); // is grounded true or false
        isOnWall = Physics2D.OverlapBox(frontPos.position, checkVectorWall, 0f, whatIsGround);
        horizontalAxis = Input.GetAxisRaw("Horizontal"); // obtain values of where is moving, -1 left, 0 idle, 1 right
        isSliding = animator.GetCurrentAnimatorStateInfo(0).IsName("sliding");

        if (isGrounded && isJumping && player_physics.velocity.y < 0f)
        {
            isJumping = false;
        }

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isJumping) 
        {
            player_physics.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
            isJumping = true;
        }

        // flip sprite
        if (!isSliding)
        {
            if (horizontalAxis > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontalAxis < 0 && facingRight)
            {
                Flip();
            }

            // Limitacion de velocidad vertical
            if (player_physics.velocity.y < -maxVerticalSpeed)
                player_physics.velocity = new Vector2(player_physics.velocity.x, -maxVerticalSpeed);

            if (Input.GetKeyUp(KeyCode.Space) && isJumping)
            {
                if (player_physics.velocity.y > 0f) player_physics.velocity = Vector2.up * 0;
                isJumping = false;
            }
        }
        else {

            // Limitacion de velocidad vertical
            if (player_physics.velocity.y < -maxVerticalSpeedSliding)
                player_physics.velocity = new Vector2(player_physics.velocity.x, -maxVerticalSpeedSliding);

            if (Input.GetKeyUp(KeyCode.Space))
            {
                wallJumping = true;
                Invoke("SetWallJumpingToFalse", walljumpTime);
            }
        }

        if (wallJumping) {
            player_physics.AddForce(new Vector2(xWallForce * -horizontalAxis, 0));
            player_physics.velocity = Vector2.up * yWallForce;
        }

        // Animation
        animator.SetFloat("playerSpeed", Mathf.Abs(player_physics.velocity.x)); // Movement
        animator.SetBool("isJumping", !isGrounded); // Jump
        animator.SetFloat("playerSpeedY", player_physics.velocity.y);
        animator.SetBool("onWall", isOnWall);
        animator.SetBool("HorizontalKey", horizontalAxis != 0);
    }

    private void FixedUpdate()
    {
        // Move player
        MovePlayer();
    }

    public void MovePlayer() 
    {
        player_physics.velocity = new Vector2(horizontalAxis * movement_speed, player_physics.velocity.y);
    }

    public void Flip()
    {
        // Switch the way the player is labeled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void SetWallJumpingToFalse() {
        wallJumping = false;
    }
}
