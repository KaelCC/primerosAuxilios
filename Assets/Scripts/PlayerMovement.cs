using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isCarrying = false;
    public float speed = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // Movimiento
        movement = new Vector2(moveX, 0);

        // Animaci�n
        animator.SetBool("IsMoving", moveX != 0);

        // Voltear sprite
        if (moveX < 0)
            spriteRenderer.flipX = true;
        else if (moveX > 0)
            spriteRenderer.flipX = false;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }
}
