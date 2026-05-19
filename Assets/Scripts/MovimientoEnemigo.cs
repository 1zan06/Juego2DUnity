using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Referencias")]

    [SerializeField] private Rigidbody2D rb2D;

    [SerializeField] private Animator animator;

    [SerializeField] private EstadosEnemigo estadoActual;

    [Header("Movimiento Horizontal")]


    [SerializeField] private float velocidadDeMovimientoActual;
    [SerializeField] private float ultimaVelocidadDeMovimiento;

    [SerializeField] private Transform controladorFrente;
    private bool tocandoSueloFrente;
    [SerializeField] private LayerMask capasSuelo;
    [SerializeField] private float distanciaRayoFrente;

    [Header("Esperar")]
    [SerializeField] private float tiempoAEsperar;
    private float tiempoAEsperarActual;

    [Header("Persecución")]
    [SerializeField] private Transform jugador;
    [SerializeField] private float radioDeteccion = 2f;
    [SerializeField] private float velocidadPersecucion = 1f;

    private void Update()
    {
        tocandoSueloFrente = Physics2D.Raycast(controladorFrente.position, transform.right * -1, distanciaRayoFrente, capasSuelo);

        if (tiempoAEsperarActual > 0)
        {
            tiempoAEsperarActual -= Time.deltaTime;
        }

        float distancia = Vector2.Distance(transform.position, jugador.position);
        if (distancia < radioDeteccion)
            estadoActual = EstadosEnemigo.Perseguir;
        else if (estadoActual == EstadosEnemigo.Perseguir)
            estadoActual = EstadosEnemigo.Correr;

        ControlarAnimaciones();
    }



    private void FixedUpdate()
    {
        switch (estadoActual)
        {
            case EstadosEnemigo.Correr:
                ComportamientoCorrer();
                break;
            case EstadosEnemigo.Esperar:
                ComportaminetoEsperar();
                break;
            case EstadosEnemigo.Perseguir:
                ComportamientoPerseguir();
                break;
        }
    }

    private void ComportaminetoEsperar()
    {
        if (tiempoAEsperarActual < 0)
        {
            CambiarAEstadoCorrer();
        }
    }

    private void CambiarAEstadoCorrer()
    {
        velocidadDeMovimientoActual = ultimaVelocidadDeMovimiento * -1;
        estadoActual = EstadosEnemigo.Correr;
    }

    private void ComportamientoPerseguir()
    {
        float direccionX = jugador.position.x - transform.position.x;

        if (Mathf.Abs(direccionX) < 0.1f)
        {
            rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
            return;
        }

        float direccion = Mathf.Sign(direccionX);
        rb2D.linearVelocity = new Vector2(direccion * velocidadPersecucion, rb2D.linearVelocity.y);

        Vector3 rotacion = transform.eulerAngles;
        rotacion.y = direccion > 0 ? 180 : 0;
        transform.eulerAngles = rotacion;
    }

    private void ComportamientoCorrer()
    {
        rb2D.linearVelocity = new Vector2(velocidadDeMovimientoActual, rb2D.linearVelocity.y);

        if (tocandoSueloFrente)
        {
            Girar();
            CambiarAEstadoEsperar();
        }

        MirarEnDireccionDelMovimiento();
    }

    private void CambiarAEstadoEsperar()
    {
        ultimaVelocidadDeMovimiento = velocidadDeMovimientoActual;
        velocidadDeMovimientoActual = 0;
        rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
        estadoActual = EstadosEnemigo.Esperar;
        tiempoAEsperarActual = tiempoAEsperar;
    }

    private void MirarEnDireccionDelMovimiento()
    {
        if ((velocidadDeMovimientoActual > 0 && !mirandoALaDerecha()) || (velocidadDeMovimientoActual < 0 && mirandoALaDerecha()))
        {
            Girar();
        }
    }

    private void Girar()
    {

        Vector3 rotacion = transform.eulerAngles;
        rotacion.y = rotacion.y == 0 ? 180 : 0;
        transform.eulerAngles = rotacion;

    }

    private bool mirandoALaDerecha()
    {
        return transform.eulerAngles.y == 180;
    }

    private void ControlarAnimaciones()
    {
        animator.SetFloat("VelocidadHorizontal", Mathf.Abs(rb2D.linearVelocity.x));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorFrente.position, controladorFrente.position + distanciaRayoFrente * transform.right * -1);
    }
}
