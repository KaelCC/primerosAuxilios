using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isCarrying = false;
    public float speed = 10f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private bool isGrounded = false;
    private bool wasGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Verificar si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Detectar cambios de estado de salto
        if (!wasGrounded && isGrounded)
        {
            animator.SetBool("IsJumping", false); // Cayó al suelo
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(moveX, 0);

        animator.SetBool("IsMoving", moveX != 0);

        if (moveX < 0)
            spriteRenderer.flipX = true;
        else if (moveX > 0)
            spriteRenderer.flipX = false;

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("IsJumping", true);
        }

        wasGrounded = isGrounded; // Guardar estado anterior
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }
}
