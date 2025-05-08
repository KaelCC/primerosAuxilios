using UnityEngine;

public class FireInteraction : MonoBehaviour
{
    private GameObject fuegoCercano;

    void Update()
    {
        if (fuegoCercano != null && Input.GetKeyDown(KeyCode.E))
        {
            // Apagar el fuego (puede ser desactivar el objeto o parar la animación)
            fuegoCercano.SetActive(false);
            fuegoCercano = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("fuego"))
        {
            fuegoCercano = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("fuego"))
        {
            if (fuegoCercano == other.gameObject)
                fuegoCercano = null;
        }
    }
}
