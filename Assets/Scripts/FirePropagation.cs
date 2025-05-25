using UnityEngine;
using System.Collections;

public class FireSpread : MonoBehaviour
{
    public float tiempoParaEncender = 5f;
    public float radioPropagacion = 20f;
    public GameObject[] fuegos;
    public int maxPropagacionPorVez = 2;
    public float cooldownPropagacion = 1.5f;
    public float delayPersonalizado = 0f;

    [HideInInspector] public bool estaEncendido = false;

    private float tiempoUltimaPropagacion = -999f;
    private bool estaEnfriado = false;

    public AudioSource audioSource; // 🔊 sonido del fuego asignado desde el Inspector

    void Start()
    {
        Debug.Log(gameObject.name + " - FireSpread activo y Start() ejecutado");
    }

    public void Encender()
    {
        if (estaEncendido || estaEnfriado) return;

        estaEncendido = true;

        // 🔊 Reproducir sonido de fuego si no está sonando
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (fuegos != null && fuegos.Length > 0)
        {
            StartCoroutine(EncenderFuegosSecuencial());
        }
        else
        {
            Debug.LogWarning("No hay llamas asignadas en " + gameObject.name);
        }

        Invoke("PropagarFuego", tiempoParaEncender);
    }

    private IEnumerator EncenderFuegosSecuencial()
    {
        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] != null)
            {
                fuegos[i].SetActive(true);
                Debug.Log(gameObject.name + " activó fuego " + (i + 1));
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void PropagarFuego()
    {
        if (Time.time - tiempoUltimaPropagacion < cooldownPropagacion) return;

        tiempoUltimaPropagacion = Time.time;
        Debug.Log(gameObject.name + " propagando fuego...");

        Collider2D[] objetosCercanos = Physics2D.OverlapCircleAll(transform.position, radioPropagacion);
        int contador = 0;

        foreach (Collider2D col in objetosCercanos)
        {
            if (col.CompareTag("inflamable") && col.gameObject != gameObject)
            {
                FireSpread otro = col.GetComponent<FireSpread>();
                if (otro != null && !otro.estaEncendido)
                {
                    // Probabilidad 75% de encender
                    if (Random.value < 0.75f)
                    {
                        Debug.Log(gameObject.name + " enciende a " + col.gameObject.name);
                        StartCoroutine(EncenderConDelay(otro));
                        contador++;
                    }
                    if (contador >= maxPropagacionPorVez)
                        break;
                }
            }
        }
    }

    private IEnumerator EncenderConDelay(FireSpread objetivo)
    {
        if (objetivo.delayPersonalizado > 0f)
            yield return new WaitForSeconds(objetivo.delayPersonalizado);

        objetivo.Encender();
    }

    public void Apagar()
    {
        if (!estaEncendido) return;

        estaEncendido = false;
        estaEnfriado = true;

        // 🔇 Detener sonido
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (fuegos != null)
        {
            foreach (GameObject fuego in fuegos)
            {
                if (fuego != null)
                    fuego.SetActive(false);
            }
        }

        Invoke(nameof(ResetEnfriado), 3.5f);
        Debug.Log(gameObject.name + " se apagó y está enfriado.");
    }

    private void ResetEnfriado()
    {
        estaEnfriado = false;
        Debug.Log(gameObject.name + " ya puede volver a encenderse.");
    }

    void Update()
    {
        if (!estaEncendido)
        {
            Collider2D[] objetosCercanos = Physics2D.OverlapCircleAll(transform.position, radioPropagacion);
            foreach (Collider2D col in objetosCercanos)
            {
                if (col.CompareTag("fuego") && col.gameObject != gameObject)
                {
                    Encender();
                    break;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioPropagacion);
    }
}
