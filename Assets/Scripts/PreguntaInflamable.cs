using UnityEngine;
using TMPro;

public class PreguntaInflamable: MonoBehaviour
{
    public GameObject mensajeNumero2;
    public TextMeshProUGUI pregunta;

    void Start()
    {
        mensajeNumero2.SetActive(true);
        pregunta.text = "Al llegar al taller, encuentras un incendio causado por una chispa. Ya que una lata de un solvente inflamable se derramo.El fuego se propaga por trapos y botes de aceite. ¡Actúa rápido!";
    }
}

