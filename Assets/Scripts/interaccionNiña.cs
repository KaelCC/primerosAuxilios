using UnityEngine;
using TMPro; // Importante para TextMeshPro

public class InteraccionNina : MonoBehaviour
{
    public TextMeshProUGUI textoMensaje; // Cambiado a TextMeshProUGUI
    public float duracionMensaje = 3f;

    private bool puedeInteractuar = false;
    private bool yaSalvo = false;

    void Update()
    {
        if (puedeInteractuar && !yaSalvo && Input.GetKeyDown(KeyCode.R))
        {
            if (!HayFuegosActivos())
            {
                yaSalvo = true;
                MostrarMensaje("Has salvado correctamente a la niña");
                // Aquí puedes añadir lógica extra al salvar a la niña (por ejemplo, avanzar nivel)
            }
            else
            {
                MostrarMensaje("¡Apaga todo el fuego antes de salvar a la niña!");
            }
        }
    }

    bool HayFuegosActivos()
    {
        FireSpread[] fuegos = Object.FindObjectsByType<FireSpread>(FindObjectsSortMode.None);
        foreach (var fuego in fuegos)
        {
            if (fuego.enabled) return true;
        }
        return false;
    }

    void MostrarMensaje(string mensaje)
    {
        textoMensaje.text = mensaje;
        textoMensaje.gameObject.SetActive(true);
        CancelInvoke(nameof(OcultarMensaje));
        Invoke(nameof(OcultarMensaje), duracionMensaje);
    }

    void OcultarMensaje()
    {
        textoMensaje.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puedeInteractuar = true;
            MostrarMensaje("Presiona R para salvar a la niña");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puedeInteractuar = false;
            OcultarMensaje();
        }
    }
}
