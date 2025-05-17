using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    private bool tieneExtintor = false;
    private GameObject extintorCercano;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        animator.SetBool("IsMoving", moveX != 0);

        if (moveX < 0)
            spriteRenderer.flipX = true;
        else if (moveX > 0)
            spriteRenderer.flipX = false;

        movement = new Vector2(moveX, 0);

        // Presionar E para recoger extintor cercano
        if (Input.GetKeyDown(KeyCode.R) && extintorCercano != null && !tieneExtintor)
        {
            tieneExtintor = true;
            animator.SetTrigger("Levantar");
            animator.SetBool("TieneExtintor", true);
            extintorCercano.SetActive(false);  // Oculta el extintor en el suelo
        }

        // Q para soltar extintor
        if (Input.GetKeyDown(KeyCode.Q) && tieneExtintor)
        {
            tieneExtintor = false;
            animator.SetBool("TieneExtintor", false);
            if (extintorCercano != null)
            {
                extintorCercano.SetActive(true);
                extintorCercano.transform.position = transform.position + Vector3.right;  // Lo deja a un lado
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }

    // Detecta entrada a la zona del extintor
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Extintor"))
        {
            extintorCercano = collision.gameObject;
        }
    }

    // Detecta salida de la zona del extintor
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Extintor"))
        {
            extintorCercano = null;
        }
    }
}
