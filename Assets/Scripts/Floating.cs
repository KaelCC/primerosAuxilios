using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public float height = 0.2f; // Altura del movimiento

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Guarda la posición inicial
    }

    void Update()
    {
        // Movimiento de arriba a abajo usando seno
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
