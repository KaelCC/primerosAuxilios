using UnityEngine;

public class FireInteraction : MonoBehaviour
{
    private FireSpread fuegoCercano;

    [Header("Sistema de partículas del chorro de agua")]
    public ParticleSystem aguaParticulas;

    void Update()
    {
        if (fuegoCercano != null && Input.GetKeyDown(KeyCode.E))
        {
            // Apagar el fuego mediante FireSpread
            fuegoCercano.Apagar();
            fuegoCercano = null;

            // Activar partículas de agua
            if (aguaParticulas != null)
            {
                aguaParticulas.Play();
                Invoke("DetenerParticulas", 0.5f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("fuego"))
        {
            FireSpread fs = other.GetComponentInParent<FireSpread>();
            if (fs != null)
            {
                fuegoCercano = fs;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("fuego"))
        {
            FireSpread fs = other.GetComponentInParent<FireSpread>();
            if (fs == fuegoCercano)
            {
                fuegoCercano = null;
            }
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
