using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public Button muteButton;
    public Sprite muteIcon;  // Ícono de mute
    public Sprite unmuteIcon; // Ícono de sonido

    private bool isMuted = false;

    void Start()
    {
        // Cargar el estado de sonido guardado
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateAudioState();

        // Asignar la función al botón
        muteButton.onClick.AddListener(ToggleMute);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        UpdateAudioState();
    }

    private void UpdateAudioState()
    {
        musicSource.mute = isMuted;
        muteButton.image.sprite = isMuted ? muteIcon : unmuteIcon;
    }
}
