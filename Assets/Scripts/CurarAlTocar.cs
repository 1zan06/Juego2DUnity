using UnityEngine;

public class CurarAlTocar : MonoBehaviour
{
    [SerializeField] private int cantidadCuracion = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out VidaJugador vidaJugador))
        {
            if (vidaJugador.GetVidaActual() < vidaJugador.GetVidaMaxima())
            {
                vidaJugador.CurarVida(cantidadCuracion);
                Destroy(gameObject);
            }
        }
    }
}
