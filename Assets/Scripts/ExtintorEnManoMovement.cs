using UnityEngine;

public class ExtintorEnManoMovement : MonoBehaviour
{
    public float speed = 3f;  // Más lento porque lleva peso
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    public Transform holdPoint;  // Asignar en Inspector

    private int currentDirection = 0; // 1 = derecha, -1 = izquierda, 0 = sin movimiento

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(moveX, 0);
        animator.SetBool("IsMoving", moveX != 0);

        // Detectar solo al presionar la tecla D o A
        if (Input.GetKeyDown(KeyCode.A) && currentDirection != -1)
        {
            spriteRenderer.flipX = true;
            currentDirection = -1;

            if (holdPoint != null)
                holdPoint.localRotation = Quaternion.Euler(0, 180f, 0); // gira el holdPoint

            Debug.Log("HoldPoint girado a la IZQUIERDA (tecla A presionada)");
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentDirection != 1)
        {
            spriteRenderer.flipX = false;
            currentDirection = 1;

            if (holdPoint != null)
                holdPoint.localRotation = Quaternion.Euler(0, 0, 0); // posición normal

            Debug.Log("HoldPoint girado a la DERECHA (tecla D presionada)");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }
}
