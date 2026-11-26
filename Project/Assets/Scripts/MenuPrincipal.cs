using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string nombreEscenaNivel = "Level1";
    [SerializeField] private AudioSource audioUI;
    [SerializeField] private AudioClip sonidoBoton;

    public void Jugar()
    {
        if (audioUI != null && sonidoBoton != null) audioUI.PlayOneShot(sonidoBoton);
        SceneManager.LoadScene(nombreEscenaNivel);
    }

    public void Salir()
    {
        if (audioUI != null && sonidoBoton != null) audioUI.PlayOneShot(sonidoBoton);
        Application.Quit();
    }
}