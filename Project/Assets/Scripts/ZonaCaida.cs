using UnityEngine;

public class ZonaCaida : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.Instancia != null)
        {
            GameManager.Instancia.PerderNivel();
        }
    }
}