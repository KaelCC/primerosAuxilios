using UnityEngine;

public class Nuempty : MonoBehaviour
{
    public Renderer fondo;
    void start()
    {

    }
    void Update()
    {
        fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.015f,0)*Time.deltaTime;
    }
}
