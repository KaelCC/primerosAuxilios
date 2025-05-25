using UnityEngine;

public class EncenderAlInicio : MonoBehaviour
{
    void Start()
    {
        GetComponent<FireSpread>()?.Encender();
    }
}
