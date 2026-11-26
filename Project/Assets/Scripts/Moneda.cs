using UnityEngine;

public class Moneda : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoRecoger;
    [SerializeField] private float volumen = 0.8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (sonidoRecoger != null)
        {
            AudioSource.PlayClipAtPoint(sonidoRecoger, transform.position, volumen);
        }

        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.SumarMoneda();
        }

        Destroy(gameObject);
    }
}