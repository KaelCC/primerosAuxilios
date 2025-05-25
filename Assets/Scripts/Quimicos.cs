using UnityEngine;

public class QuimicosReaction : MonoBehaviour
{
    public Animator animator;
    public KeyCode reaccionKey = KeyCode.F;

    private bool jugadorDentro = false;
    private bool fuegoActivado = false;
    private GameObject fuegoObject;

    void Start()
    {
        // Busca al hijo llamado "Fuego"
        Transform fuegoTransform = transform.Find("Fuego");

        if (fuegoTransform == null)
        {
            Debug.LogWarning("No se encontró un objeto hijo llamado 'Fuego'.");
        }
        else
        {
            fuegoObject = fuegoTransform.gameObject;
            fuegoObject.SetActive(false);
            Debug.Log("Fuego encontrado como hijo y desactivado.");
        }
    }

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(reaccionKey))
        {
            animator.SetTrigger("Reaccionar");
            fuegoActivado = false;
            Debug.Log("Tecla 'F' presionada. Animación 'Reaccionar' activada.");
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!fuegoActivado && stateInfo.IsName("Reaccionar") && stateInfo.normalizedTime >= 1f)
        {
            ActivarFuego();
            fuegoActivado = true;
            Debug.Log("Animación finalizada. Fuego activado.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            Debug.Log("Jugador entró al área.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            Debug.Log("Jugador salió del área.");
        }
    }

    void ActivarFuego()
    {
        if (fuegoObject != null)
        {
            fuegoObject.SetActive(true);
            Debug.Log("🔥 Fuego ACTIVADO.");
        }
    }
}







