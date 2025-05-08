using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image transitionImage; // Arrastra la imagen de UI aquí
    public float fadeSpeed = 1f;

    void Start()
    {
        transitionImage.color = new Color(0, 0, 0, 0); // Inicia completamente transparente
        transitionImage.gameObject.SetActive(false); // Se desactiva hasta que se use
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName)
    {
        transitionImage.gameObject.SetActive(true);
        for (float t = 0; t <= 1; t += Time.deltaTime * fadeSpeed)
        {
            transitionImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
}
