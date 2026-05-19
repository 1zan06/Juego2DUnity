using UnityEngine;

public class DañoAlTocar : MonoBehaviour
{
    [SerializeField] private int dañoPorToque;
    [SerializeField] private float tiempoEntreGolpes = 1f;
    [SerializeField] private AudioClip sonidoDaño;

    private float timerGolpe = 0f;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = FindFirstObjectByType<Player>().GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (timerGolpe > 0)
            timerGolpe -= Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        AplicarDaño(collision);
    }

    private void AplicarDaño(Collider2D collision)
    {
        if (timerGolpe > 0) return;

        if (collision.TryGetComponent(out VidaJugador vidaJugador))
        {
            vidaJugador.TomarDaño(dañoPorToque);
            timerGolpe = tiempoEntreGolpes;

            if (sonidoDaño != null && audioSource != null)
                audioSource.PlayOneShot(sonidoDaño);
        }
    }
}
