using System.Collections;
using UnityEngine;

public class EfectoDaño : MonoBehaviour
{
    [SerializeField] private float duracionParpadeo = 1f;
    [SerializeField] private float velocidadParpadeo = 0.1f;
    [SerializeField] private Color colorDaño = Color.red;

    private SpriteRenderer spriteRenderer;
    private VidaJugador vidaJugador;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        vidaJugador = GetComponent<VidaJugador>();

        vidaJugador.JugadorTomoDaño += _ => StartCoroutine(Parpadear());
    }

    private void OnDisable()
    {
        vidaJugador.JugadorTomoDaño -= _ => StartCoroutine(Parpadear());
    }

    private IEnumerator Parpadear()
    {
        vidaJugador.ActivarInvencibilidad();
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionParpadeo)
        {
            spriteRenderer.color = colorDaño;
            yield return new WaitForSeconds(velocidadParpadeo);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(velocidadParpadeo);
            tiempoTranscurrido += velocidadParpadeo * 2;
        }
        spriteRenderer.color = Color.white;
    }
}
