using UnityEngine;

public class FireInteraction : MonoBehaviour
{
    private GameObject fuegoCercano;

    [Header("Sistema de partículas del chorro de agua")]
    public ParticleSystem aguaParticulas;

    void Update()
    {
        if (fuegoCercano != null && Input.GetKeyDown(KeyCode.E))
        {
            // Apagar el fuego
            fuegoCercano.SetActive(false);
            fuegoCercano = null;

            // Activar partículas de agua
            if (aguaParticulas != null)
            {
                aguaParticulas.Play();
                Invoke("DetenerParticulas", 0.5f); // Detiene después de 1.5 segundos
            }
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
        if (other.CompareTag("fuego") && fuegoCercano == other.gameObject)
        {
            fuegoCercano = null;
        }
    }

    void DetenerParticulas()
    {
        if (aguaParticulas != null)
        {
            aguaParticulas.Stop();
        }
    }
}
