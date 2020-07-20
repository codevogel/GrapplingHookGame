using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    public float movementSpeed;
    public float gravity;
    public float maxFallingSpeed;

    private Rigidbody2D rigidbody;
    private Vector2 direction;
    private Vector2 feet;

    public float verticalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        feet = new Vector2(0, GetComponent<Collider2D>().bounds.extents.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Grounded)
        {
            Debug.Log("grounded");
            verticalVelocity = 0;
            if (Input.GetButton("Jump"))
            {
                verticalVelocity = 10;
            }
        }
        direction = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, verticalVelocity);

    }

    private void FixedUpdate()
    {
        moveTowards(direction);

        verticalVelocity += gravity * Time.fixedDeltaTime;
        if (verticalVelocity < -maxFallingSpeed)
        {
            verticalVelocity = -maxFallingSpeed;
        }
    }

    private void moveTowards(Vector2 direction)
    {
        rigidbody.MovePosition((Vector2)transform.position + (direction * Time.fixedDeltaTime));
    }

    public bool Grounded
    {
        get {
            return Physics2D.Raycast((Vector2) transform.position - feet, Vector2.down, 0.1f, LayerMask.GetMask("Floor")) ; 
        }
    }
}
