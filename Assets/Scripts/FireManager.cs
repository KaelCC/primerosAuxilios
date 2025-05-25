using UnityEngine;

public class FireManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Update()
    {
        FireSpread[] fuegos = FindObjectsOfType<FireSpread>();
        bool hayFuegoActivo = false;

        foreach (FireSpread fuego in fuegos)
        {
            if (fuego.estaEncendido)
            {
                hayFuegoActivo = true;
                break;
            }
        }

        if (!hayFuegoActivo && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("🔥 Todos los fuegos apagados. Música detenida.");
        }
    }
}
