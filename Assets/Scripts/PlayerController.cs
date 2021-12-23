using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Jumping
    private bool isGrounded;
    public Transform feetPos;
    private float checkRadius;
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
            // ... flip the player.
            Flip();
        }

        // Animation
        animator.SetFloat("playerSpeed", Mathf.Abs(horizontalAxis));

        // Jump
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)) 
        {
            player_physics.velocity = Vector2.up * jumpForce;
        }
    }

    private void FixedUpdate()
    {
        player_physics.velocity = new Vector2(horizontalAxis * movement_speed, player_physics.velocity.y);
    }

   

    private void Flip()
    {
        // Switch the way the player is labeled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
