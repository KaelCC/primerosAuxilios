using UnityEngine;

public class MoverCamaraANina : MonoBehaviour
{
    public Transform camaraPrincipal;  // la c�mara que queremos mover
    public Transform objetivoNina;
    public Transform objetivoJugador;
    public float duracionTransicion = 1f; // tiempo para mover la c�mara suavemente
    public float duracionEnfoque = 3f;   // tiempo para seguir a la ni�a
    public float velocidadMovimiento = 5f;

    private bool yaCambio = false;
    private MonoBehaviour scriptSeguirJugador; // referencia al script que mantiene la c�mara siguiendo al jugador

    void Start()
    {
        // Intentar obtener el script que hace que la c�mara siga al jugador
        // Cambia "CameraFollow" por el nombre real de tu script
        scriptSeguirJugador = camaraPrincipal.GetComponent<MonoBehaviour>();

        StartCoroutine(CambiarEnfoque());
    }

    System.Collections.IEnumerator CambiarEnfoque()
    {
        if (yaCambio) yield break;
        yaCambio = true;

        // Deshabilitar script que sigue al jugador para poder mover c�mara manualmente
        if (scriptSeguirJugador != null)
            scriptSeguirJugador.enabled = false;

        // 1. Mover c�mara a la posici�n de la ni�a suavemente
        yield return MoverCamara(camaraPrincipal, objetivoNina.position, duracionTransicion);

        // 2. Seguir a la ni�a durante duracionEnfoque segundos
        float tiempo = 0f;
        while (tiempo < duracionEnfoque)
        {
            camaraPrincipal.position = Vector3.Lerp(camaraPrincipal.position, objetivoNina.position, Time.deltaTime * velocidadMovimiento);
            tiempo += Time.deltaTime;
            yield return null;
        }

        // 3. Mover c�mara de vuelta al jugador suavemente
        yield return MoverCamara(camaraPrincipal, objetivoJugador.position, duracionTransicion);

        // 4. Volver a habilitar el script para que la c�mara siga al jugador normalmente
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
