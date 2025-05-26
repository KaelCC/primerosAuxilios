using UnityEngine;
using TMPro;

public class MensajeInicial : MonoBehaviour
{
    public GameObject panelMensaje;
    public TextMeshProUGUI texto;

    void Start()
    {
        panelMensaje.SetActive(true);
        texto.text = "Al llegar al taller, encuentras un incendio causado por una chispa. Que fue causada por una lata de solvente. El fuego se propaga por trapos y botes de aceite. ¡Actúa rápido!";
    }
}
