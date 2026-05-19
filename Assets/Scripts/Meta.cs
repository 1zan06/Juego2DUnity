using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class Meta : MonoBehaviour
{
    [SerializeField] private GameObject pantallaFinal;
    [SerializeField] private float velocidadFade = 1.5f;

    private CanvasGroup canvasGroup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out VidaJugador jugador))
        {
            MostrarPantallaFinal();
        }
    }

    private void MostrarPantallaFinal()
    {
        pantallaFinal.SetActive(true);
        canvasGroup = pantallaFinal.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = pantallaFinal.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime * velocidadFade;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
