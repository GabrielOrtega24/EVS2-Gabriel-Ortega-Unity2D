using UnityEngine;

public class Moneda : MonoBehaviour
{
    // Se ejecuta automáticamente cuando otro Collider2D entra en contacto con este Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que tocó la moneda tiene el Tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Aquí puedes agregar lógica adicional (sumar puntaje, reproducir sonido, etc.)
            Debug.Log("¡Moneda recogida!");

            // Destruye este objeto moneda de la escena
            Destroy(gameObject);
        }
    }
}