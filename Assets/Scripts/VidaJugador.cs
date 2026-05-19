using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VidaJugador : MonoBehaviour
{
    public Action<int> JugadorTomoDaño;
    public Action<int> JugadorSeCuro;

    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaActual;
    [SerializeField] private float tiempoInvencibilidad = 1f;
    [SerializeField] private float tiempoAntesDeRespawn = 0.1f;

    private bool esInvencible = false;

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }


    public void TomarDaño(int daño)
    {
        if (esInvencible) return;

        vidaActual = Mathf.Clamp(vidaActual - daño, 0, vidaMaxima);

        JugadorTomoDaño?.Invoke(vidaActual);

        if (vidaActual <= 0)
        {
            StartCoroutine(Morir());
        }
    }

    private IEnumerator Morir()
    {
        esInvencible = true;
        yield return new WaitForSeconds(tiempoAntesDeRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ActivarInvencibilidad()
    {
        StartCoroutine(InvencibilidadTemporal());
    }

    private IEnumerator InvencibilidadTemporal()
    {
        esInvencible = true;
        yield return new WaitForSeconds(tiempoInvencibilidad);
        esInvencible = false;
    }

    public void CurarVida(int curacion)
    {

        vidaActual = Mathf.Clamp(vidaActual + curacion, 0, vidaMaxima);

        JugadorSeCuro?.Invoke(vidaActual);
    }

    private void DestruirJugador()
    {
        Destroy(gameObject);
    }

    public int GetVidaMaxima() => vidaMaxima;
    public int GetVidaActual() => vidaActual;
}
