using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movement_speed = 3f;
    private Rigidbody2D player_physics;
    private Vector2 movement;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        player_physics = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontalAxis, player_physics.velocity.y);

        if (horizontalAxis > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalAxis < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction)
    {
        player_physics.velocity = direction * movement_speed;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
