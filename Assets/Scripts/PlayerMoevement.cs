using UnityEngine;

public class Standing_still : MonoBehaviour
{
    public float velocidad = 15f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject EXTINTORENMANOS;

    private bool tieneExtintor = false;
    private bool puedeRecoger = false;
    private GameObject extintorSuelo;
    private Vector2 movimiento;
    private Vector3 escalaOriginal;

    void Start()
    {
        escalaOriginal = transform.localScale;

        if (EXTINTORENMANOS != null)
            EXTINTORENMANOS.SetActive(false);
    }

    void Update()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal");
        movimiento.y = 0; // evita que "vuele"

        if (movimiento.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movimiento.x) * Mathf.Abs(escalaOriginal.x), escalaOriginal.y, escalaOriginal.z);
        }

        // Animaciones dependiendo si tiene extintor
        if (!tieneExtintor)
        {
            animator.SetBool("IsMoving", movimiento != Vector2.zero);
        }
        else
        {
            animator.SetBool("IsWalking", movimiento != Vector2.zero);
        }

        // Presionar R para recoger el extintor si est� cerca
        if (puedeRecoger && !tieneExtintor && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(LevantarExtintor());
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimiento * velocidad * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Extintor") && !tieneExtintor)
        {
            puedeRecoger = true;
            extintorSuelo = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Extintor"))
        {
            puedeRecoger = false;
        }
    }

    private System.Collections.IEnumerator LevantarExtintor()
    {
        movimiento = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        animator.SetTrigger("Levantar");

        yield return new WaitForSeconds(0.1f); // Tiempo para animaci�n de levantar

        extintorSuelo.SetActive(false);
        EXTINTORENMANOS.SetActive(true);

        tieneExtintor = true;
        animator.SetBool("TieneExtintor", true);
    }
}
