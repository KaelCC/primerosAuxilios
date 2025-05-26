using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class controladorjuego : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;
    [SerializeField] private Slider slider;

    private float tiempoActual;

    private bool tiempoActivado = false;

    private void Start()
    {
        ActivarTemporizador();
    }
    private void Update()
    {
        if (tiempoActivado)
        {
            cambiarContador();
        }
    }
    private void cambiarContador()
    {
        tiempoActual -= Time.deltaTime;
        if (tiempoActual >= 0)
        {
            slider.value = tiempoActual;
        }
        if (tiempoActual <= 0)
        {
            Debug.Log("Derrota");
            tiempoActivado = false;
        }
    }
    private void cambiarTemporizador(bool estado)
    {
        tiempoActivado = estado;
    }
    public void ActivarTemporizador()
    {
        tiempoActual = tiempoMaximo;
        slider.maxValue = tiempoMaximo;
        cambiarTemporizador(true);
    }
    public void DesactivarTemporizador()
    {
        cambiarTemporizador(false);
    }
}
