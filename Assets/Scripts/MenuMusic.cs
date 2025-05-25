using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private static MenuMusic instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void StopMenuMusic()
    {
        if (instance != null && instance.audioSource.isPlaying)
        {
            instance.audioSource.Stop();
        }
    }
}
