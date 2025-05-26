using UnityEngine;

public class ActivadorEncuestaClick : MonoBehaviour
{
    public GameObject panelCuracion; // ← Panel de la encuesta

    void OnMouseDown()
    {
        Debug.Log("Hiciste click sobre el mecánico");

        if (panelCuracion != null)
        {
            panelCuracion.SetActive(true); // ← Activa el panel de encuesta
        }
        else
        {
            Debug.LogWarning("Panel de curación no asignado.");
        }
    }
}



