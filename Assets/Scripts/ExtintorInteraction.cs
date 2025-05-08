using UnityEngine;
using TMPro;

public class ExtintorInteraction : MonoBehaviour
{
    public GameObject infoPanel; // Aquí va el texto + imagen juntos

    void Start()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        if (infoPanel != null)
        {
            // Alterna entre mostrar y ocultar el panel (imagen + texto)
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
    }
}
