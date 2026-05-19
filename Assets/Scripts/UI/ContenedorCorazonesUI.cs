using UnityEngine;

public class ContenedorCorazonesUI : MonoBehaviour
{
    [SerializeField] private CorazonUI[] corazones;
    [SerializeField] private VidaJugador vidaJugador;

    private void Start()
    {
        vidaJugador = FindFirstObjectByType<VidaJugador>();

        ActivaCorazones(vidaJugador.GetVidaActual());

        vidaJugador.JugadorTomoDaño += ActivaCorazones;
        vidaJugador.JugadorSeCuro += ActivaCorazones;

    }

    private void OnDisable()
    {
        vidaJugador.JugadorTomoDaño -= ActivaCorazones;
        vidaJugador.JugadorSeCuro -= ActivaCorazones;
    }

    private void ActivaCorazones(int vidaActual)
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vidaActual)
            {
                corazones[i].ActivarCorazon();

            }
            else
            {
                corazones[i].DesactivarCorazon();
            }
        }

    }
}
