using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    [Header("Interfaz (UI)")]
    [SerializeField] private TextMeshProUGUI textoMonedas;
    [SerializeField] private GameObject panelFin;
    [SerializeField] private TextMeshProUGUI textoResultado;

    [Header("Audio UI")]
    [SerializeField] private AudioSource audioUI;
    [SerializeField] private AudioClip sonidoBoton;

    private int monedasRecolectadas;
    private int totalMonedas;
    private bool juegoTerminado;

    private void Awake()
    {
        Instancia = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        totalMonedas = GameObject.FindObjectsByType<Moneda>(FindObjectsSortMode.None).Length;
        if (panelFin != null) panelFin.SetActive(false);
        ActualizarHUD();
    }

    public void SumarMoneda()
    {
        if (juegoTerminado) return;

        monedasRecolectadas++;
        ActualizarHUD();

        if (monedasRecolectadas >= totalMonedas)
        {
            FinalizarJuego("¡NIVEL COMPLETADO!");
        }
    }

    public void PerderNivel()
    {
        if (juegoTerminado) return;
        FinalizarJuego("HAS CAÍDO - FIN DEL JUEGO");
    }

    private void FinalizarJuego(string mensaje)
    {
        juegoTerminado = true;
        if (textoResultado != null) textoResultado.text = mensaje;
        if (panelFin != null) panelFin.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ActualizarHUD()
    {
        if (textoMonedas != null)
        {
            textoMonedas.text = $"Medallones: {monedasRecolectadas} / {totalMonedas}";
        }
    }

    public void ReiniciarNivel()
    {
        ReproducirSonidoUI();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAMenuPrincipal()
    {
        ReproducirSonidoUI();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void ReproducirSonidoUI()
    {
        if (audioUI != null && sonidoBoton != null)
        {
            audioUI.PlayOneShot(sonidoBoton);
        }
    }
}