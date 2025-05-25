using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireMusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public float fadeDuration = 2f;
    public float fuegoCheckInterval = 1f; // Cada cuántos segundos actualizar la lista de fuegos

    private List<FireSpread> fuegos = new List<FireSpread>();
    private bool fadeInProgress = false;

    void Start()
    {
        MenuMusic.StopMenuMusic(); // Detener música del menú al iniciar nivel
        StartCoroutine(ActualizarFuegosPeriodicamente());

        if (!musicSource.isPlaying && HayFuegoActivo())
        {
            musicSource.volume = 1f;
            musicSource.Play();
        }
    }

    void Update()
    {
        if (HayFuegoActivo())
        {
            if (!musicSource.isPlaying && !fadeInProgress)
            {
                musicSource.volume = 1f;
                musicSource.Play();
                Debug.Log("🔥 Música encendida porque hay fuego activo.");
            }
        }
        else
        {
            if (musicSource.isPlaying && !fadeInProgress)
            {
                Debug.Log("❄️ No hay fuego activo. Iniciando fade.");
                StartCoroutine(FadeOutAndStop());
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
        return false;
    }

    IEnumerator FadeOutAndStop()
    {
        fadeInProgress = true;

        float startVolume = musicSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.Stop();
        Debug.Log("🎵 Música detenida.");
        fadeInProgress = false;
    }

    IEnumerator ActualizarFuegosPeriodicamente()
    {
        while (true)
        {
            fuegos.Clear();
            fuegos.AddRange(Object.FindObjectsByType<FireSpread>(FindObjectsSortMode.None));
            yield return new WaitForSeconds(fuegoCheckInterval);
        }
    }
}
