using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Jumping
    private bool isGrounded;
    public Transform feetPos;
    public Vector2 checkVector;
    public float jumpForce = 5f;

    // Movement
    public float movement_speed = 3f;
    private Vector2 movement;
    private float horizontalAxis;
    public LayerMask whatIsGround;

    // Physics
    private Rigidbody2D player_physics;

    //sprite & animations
    private bool facingRight = true;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player_physics = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal"); // obtain values of where is moving, -1 left, 0 idle, 1 right

        // flip sprite
        if (horizontalAxis > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalAxis < 0 && facingRight)
        {
            Flip();
        }

        // Jump
        isGrounded = Physics2D.OverlapBox(feetPos.position, checkVector, 0f, whatIsGround); 

        if (isGrounded  && Input.GetKeyDown(KeyCode.Space)) 
        {
            player_physics.velocity = Vector2.up * jumpForce;
        }

        // Animation
        animator.SetFloat("playerSpeed", Mathf.Abs(player_physics.velocity.x)); // Movement
        animator.SetBool("isJumping", !isGrounded); // Jump
        animator.SetFloat("playerSpeedY", player_physics.velocity.y);
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
}
