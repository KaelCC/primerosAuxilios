using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenuManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayGame()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        // Retrasa el cambio de escena para que el sonido se escuche antes
        Invoke("LoadScene", 1f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}