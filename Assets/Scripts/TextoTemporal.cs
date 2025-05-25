using UnityEngine;

public class TextoTemporal : MonoBehaviour
{
    public float tiempoVisible = 5f;

    void Start()
    {
        Invoke("DesactivarTexto", tiempoVisible);
    }

    void DesactivarTexto()
    {
        gameObject.SetActive(false);
    }
}
