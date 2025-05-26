using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncuestaCuracion : MonoBehaviour
{
    [Header("Paneles y texto")]
    public GameObject panelEncuesta;
    public GameObject panelRescate;
    public TextMeshProUGUI textoResultado;

    [Header("Objetos de curación (desactivados al inicio)")]
    public GameObject gasa;
    public GameObject sueroFisiologico;

    [Header("Jugador")]
    public GameObject jugador;

    private bool haRespondidoBien = false;

    public void MostrarEncuesta()
    {
        panelEncuesta.SetActive(true);
    }

    public void Responder(string opcion)
    {
        if (opcion == "Suero+Gasa")
        {
            textoResultado.text = "¡Correcto! Has usado el equipo adecuado.";

            // Activar objetos y adherirlos al jugador
            if (gasa != null)
            {
                gasa.SetActive(true);
                gasa.transform.SetParent(jugador.transform);
            }

            if (sueroFisiologico != null)
            {
                sueroFisiologico.SetActive(true);
                sueroFisiologico.transform.SetParent(jugador.transform);
            }

            haRespondidoBien = true;
            Invoke("OcultarEncuesta", 3f);
        }
        else
        {
            textoResultado.text = "Incorrecto. Se recomienda usar suero fisiológico y gasa estéril.";
            Invoke("LimpiarTexto", 4f);
        }
    }

    void OcultarEncuesta()
    {
        panelEncuesta.SetActive(false);
        textoResultado.text = "";
    }

    void LimpiarTexto()
    {
        textoResultado.text = "";
    }

    // Esto se llama al volver a tocar al mecánico
    public void VerificarRescate()
    {
        if (haRespondidoBien)
        {
            panelRescate.SetActive(true);
        }
    }
}
