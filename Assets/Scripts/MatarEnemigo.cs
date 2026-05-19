using UnityEngine;

public class MatarEnemigo : MonoBehaviour
{
    [SerializeField] private float fuerzaRebote = 3f;
    [SerializeField] private AudioClip sonidoMuerte;
    [SerializeField] private int puntosPorMatar = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rbJugador = collision.GetComponent<Rigidbody2D>();

        if (rbJugador == null) return;

        if (rbJugador.linearVelocity.y < 0 &&
            collision.transform.position.y > transform.parent.position.y)
        {
            rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, fuerzaRebote);

            if (sonidoMuerte != null)
                AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position);

            Player jugador = collision.GetComponent<Player>();
            if (jugador != null)
            {
                jugador.coins += puntosPorMatar;
                jugador.coinText.text = jugador.coins.ToString();
            }

            Destroy(transform.parent.gameObject);
        }
    }
}
