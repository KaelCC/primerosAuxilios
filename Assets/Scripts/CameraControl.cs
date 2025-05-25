using UnityEngine;

public class MoverCamaraANina : MonoBehaviour
{
    public Transform camaraPrincipal;  // la cámara que queremos mover
    public Transform objetivoNina;
    public Transform objetivoJugador;
    public float duracionTransicion = 1f; // tiempo para mover la cámara suavemente
    public float duracionEnfoque = 3f;   // tiempo para seguir a la niña
    public float velocidadMovimiento = 5f;

    private bool yaCambio = false;
    private MonoBehaviour scriptSeguirJugador; // referencia al script que mantiene la cámara siguiendo al jugador

    void Start()
    {
        // Intentar obtener el script que hace que la cámara siga al jugador
        // Cambia "CameraFollow" por el nombre real de tu script
        scriptSeguirJugador = camaraPrincipal.GetComponent<MonoBehaviour>();

        StartCoroutine(CambiarEnfoque());
    }

    System.Collections.IEnumerator CambiarEnfoque()
    {
        if (yaCambio) yield break;
        yaCambio = true;

        // Deshabilitar script que sigue al jugador para poder mover cámara manualmente
        if (scriptSeguirJugador != null)
            scriptSeguirJugador.enabled = false;

        // 1. Mover cámara a la posición de la niña suavemente
        yield return MoverCamara(camaraPrincipal, objetivoNina.position, duracionTransicion);

        // 2. Seguir a la niña durante duracionEnfoque segundos
        float tiempo = 0f;
        while (tiempo < duracionEnfoque)
        {
            camaraPrincipal.position = Vector3.Lerp(camaraPrincipal.position, objetivoNina.position, Time.deltaTime * velocidadMovimiento);
            tiempo += Time.deltaTime;
            yield return null;
        }

        // 3. Mover cámara de vuelta al jugador suavemente
        yield return MoverCamara(camaraPrincipal, objetivoJugador.position, duracionTransicion);

        // 4. Volver a habilitar el script para que la cámara siga al jugador normalmente
        if (scriptSeguirJugador != null)
            scriptSeguirJugador.enabled = true;
    }

    System.Collections.IEnumerator MoverCamara(Transform camara, Vector3 destino, float duracion)
    {
        Vector3 inicio = camara.position;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            camara.position = Vector3.Lerp(inicio, destino, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        camara.position = destino;
    }
}
