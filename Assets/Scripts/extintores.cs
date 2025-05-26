using UnityEngine;
using TMPro;

public class ControlCuestionario : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelBotones;
    public GameObject panelMensajeCorrecto;
    public GameObject panelMensajeIncorrecto;

    [Header("Texto de Mensaje Incorrecto")]
    public TextMeshProUGUI textoIncorrecto;

    [Header("Objeto extintor que se activa al responder bien")]
    public GameObject extintor; // Asignar desde el Inspector

    [TextArea]
    public string mensajeExplicacion = "Ese extintor no es adecuado para fuegos de tipo B. Usa uno como CO2 o Espuma.";

    public float tiempoVisibleMensaje = 5f;
    public float tiempoAntesDeOcultarBotones = 3f;

    public void Responder(string tipo)
    {
        if (tipo == "ESPUMA")
        {
            panelMensajeCorrecto.SetActive(true);
            Invoke("OcultarMensajeCorrecto", tiempoVisibleMensaje);
            Invoke("OcultarBotones", tiempoAntesDeOcultarBotones);

            if (extintor != null)
                extintor.SetActive(true);
            else
                Debug.LogWarning("No se ha asignado el extintor en el Inspector.");
        }
        else
        {
            panelMensajeIncorrecto.SetActive(true);
            textoIncorrecto.text = mensajeExplicacion;
            Invoke("OcultarMensajeIncorrecto", tiempoVisibleMensaje);
        }
    }

    void OcultarMensajeCorrecto()
    {
        panelMensajeCorrecto.SetActive(false);
    }

    void OcultarMensajeIncorrecto()
    {
        panelMensajeIncorrecto.SetActive(false);
    }

    void OcultarBotones()
    {
        if (panelBotones != null)
            panelBotones.SetActive(false);
    }
}
