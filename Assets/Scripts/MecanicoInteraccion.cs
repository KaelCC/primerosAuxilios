using UnityEngine;

public class MecanicoInteraccion : MonoBehaviour
{
    public EncuestaCuracion encuesta;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!encuesta.gameObject.activeSelf)
                encuesta.MostrarEncuesta();
            else
                encuesta.VerificarRescate();
        }
    }
}

