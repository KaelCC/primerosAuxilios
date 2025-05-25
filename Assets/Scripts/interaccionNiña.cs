using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteraccionNinaConPanel : MonoBehaviour
{
    [Header("Paneles de UI")]
    public GameObject panelFaltanLlamas;       // Panel que dice "Faltan llamas por apagar"
    public GameObject panelRescateExitoso;     // Panel que dice "Has rescatado a la niña"
    public float duracionMensaje = 60f;

    private bool puedeInteractuar = false;
    private bool yaSalvada = false;
    private List<FireSpread> fuegos = new List<FireSpread>();

    void Start()
    {
        OcultarAmbosPaneles();
        StartCoroutine(ActualizarFuegosPeriodicamente());
        Debug.Log("🔄 Iniciado y paneles ocultos.");
    }

    void Update()
    {
        if (puedeInteractuar && !yaSalvada && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("🟡 Presionaste R estando en el área de interacción.");
            if (!HayFuegoActivo())
            {
                yaSalvada = true;
                MostrarPanel(panelRescateExitoso);
                Debug.Log("✅ Has rescatado correctamente a la niña.");
            }
            else
            {
                MostrarPanel(panelFaltanLlamas);
                Debug.Log("🚨 Aún hay fuego activo.");
            }
        }
    }

    bool HayFuegoActivo()
    {
        foreach (FireSpread fuego in fuegos)
        {
            if (fuego != null && fuego.estaEncendido)
            {
                Debug.Log("🔥 Fuego activo en: " + fuego.gameObject.name);
                return true;
            }
        }
        Debug.Log("✅ No hay fuegos activos.");
        return false;
    }

    IEnumerator ActualizarFuegosPeriodicamente()
    {
        while (true)
        {
            fuegos.Clear();
            fuegos.AddRange(Object.FindObjectsByType<FireSpread>(FindObjectsSortMode.None));
            yield return new WaitForSeconds(1f);
        }
    }

    void MostrarPanel(GameObject panel)
    {
        OcultarAmbosPaneles();
        if (panel != null)
        {
            panel.SetActive(true);
            Debug.Log("📋 Mostrando panel: " + panel.name);
            StartCoroutine(OcultarPanelTrasTiempo(panel, duracionMensaje));
        }
    }

    IEnumerator OcultarPanelTrasTiempo(GameObject panel, float segundos)
    {
        yield return new WaitForSeconds(segundos);
        panel.SetActive(false);
        Debug.Log("⏱️ Ocultado panel: " + panel.name);
    }

    void OcultarAmbosPaneles()
    {
        if (panelFaltanLlamas != null) panelFaltanLlamas.SetActive(false);
        if (panelRescateExitoso != null) panelRescateExitoso.SetActive(false);
        Debug.Log("🚫 Ambos paneles ocultos.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            puedeInteractuar = true;
            Debug.Log("✅ Jugador entró en zona de interacción.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            puedeInteractuar = false;
            OcultarAmbosPaneles();
            Debug.Log("❌ Jugador salió de la zona de interacción.");
        }
    }
}
