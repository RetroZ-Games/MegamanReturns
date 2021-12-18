using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movement_speed = 1f;
    private Rigidbody2D player_physics;

    // Start is called before the first frame update
    void Start()
    {
        player_physics = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        player_physics.velocity = new Vector2(x * movement_speed * Time.deltaTime, 0);
    }
}
