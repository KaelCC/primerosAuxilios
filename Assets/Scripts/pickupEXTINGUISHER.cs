using UnityEngine;
using System.Collections;

public class ExtintorPickup : MonoBehaviour
{
    public Transform holdPoint; // Lugar donde se sujeta el extintor (mano)
    public float holdPointOffsetX = 12f; // Distancia para mover el holdPoint a la izquierda o derecha
    public float pickupRange = 2f;
    public KeyCode pickupKey = KeyCode.Q;
    public LayerMask extintorLayer;

    public Animator animator;
    public PlayerMovement playerMovement; // Tu script de movimiento

    private GameObject currentExtintor;
    private bool IsCarrying = false;

    // Referencia al script de floating del extintor actual
    private FloatingEffect floatingScript;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicializa holdPoint en lado correcto al inicio (derecha)
        holdPoint.localPosition = new Vector3(holdPointOffsetX, holdPoint.localPosition.y, holdPoint.localPosition.z);
    }

    void Update()
    {
        // Actualizar la posición del holdPoint según la dirección del personaje
        if (spriteRenderer.flipX)
        {
            // Mira a la izquierda, holdPoint a la izquierda
            holdPoint.localPosition = new Vector3(-holdPointOffsetX, holdPoint.localPosition.y, holdPoint.localPosition.z);
            holdPoint.localRotation = Quaternion.identity;  // sin rotación
        }
        else
        {
            // Mira a la derecha, holdPoint a la derecha
            holdPoint.localPosition = new Vector3(holdPointOffsetX, holdPoint.localPosition.y, holdPoint.localPosition.z);
            holdPoint.localRotation = Quaternion.identity;  // sin rotación
        }

        // Pickup / Drop extintor
        if (Input.GetKeyDown(pickupKey))
        {
            if (!IsCarrying)
            {
                TryPickUpExtintor();
            }
            else
            {
                DropExtintor();
            }
        }
    }

    void TryPickUpExtintor()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, pickupRange, extintorLayer);
        if (hit != null && hit.CompareTag("Extintor"))
        {
            StartCoroutine(PickUpWithAnimation(hit.gameObject));
        }
    }

    IEnumerator PickUpWithAnimation(GameObject extintor)
    {
        animator.SetTrigger("Levantar");

        yield return new WaitForSeconds(0.7f);

        IsCarrying = true;
        currentExtintor = extintor;

        // Desactivar efecto floating
        floatingScript = extintor.GetComponent<FloatingEffect>();
        if (floatingScript != null)
            floatingScript.enabled = false;

        Rigidbody2D rb = extintor.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Kinematic;

        extintor.transform.SetParent(holdPoint);
        extintor.transform.localPosition = Vector3.zero;
        extintor.transform.localRotation = Quaternion.identity;

        animator.SetBool("IsCarrying", true);
        playerMovement.isCarrying = true;
    }

    void DropExtintor()
    {
        if (!currentExtintor) return;

        IsCarrying = false;

        Rigidbody2D rb = currentExtintor.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Dynamic;

        if (floatingScript != null)
            floatingScript.enabled = true;

        currentExtintor.transform.SetParent(null);
        // Al soltar, lo ponemos un poco al lado opuesto al que mira el personaje para que no se sobrepongan
        Vector3 dropOffset = spriteRenderer.flipX ? Vector3.right : Vector3.left;
        currentExtintor.transform.position = transform.position + dropOffset;

        animator.SetBool("IsCarrying", false);
        playerMovement.isCarrying = false;

        currentExtintor = null;
        floatingScript = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
