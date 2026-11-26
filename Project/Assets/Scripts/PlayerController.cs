using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento y Animación")]
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private Animator animator;

    [Header("Configuración de Salto")]
    [SerializeField] private float fuerzaSalto = 10f;
    [SerializeField] private float distanciaLineaSuelo = 2.4f;
    [SerializeField] private LayerMask capaSuelo;
    [SerializeField] private bool tocandoElSuelo = false;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoSalto;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- 1. MOVIMIENTO HORIZONTAL ---
        float velocidadH = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;

        if (velocidadH > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velocidadH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Vector3 movimiento = transform.position;
        transform.position = new Vector3(movimiento.x + velocidadH, movimiento.y, movimiento.z);

        // --- 2. DETECCIÓN DE SUELO Y SALTO ---
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanciaLineaSuelo, capaSuelo);
        tocandoElSuelo = hit.collider != null;

        if (Input.GetKeyDown(KeyCode.Space) && tocandoElSuelo)
        {
            rb.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);

            if (audioSource != null && sonidoSalto != null)
            {
                audioSource.PlayOneShot(sonidoSalto);
            }
        }

        // --- 3. ACTUALIZAR PARÁMETROS DEL ANIMATOR ---
        if (animator != null)
        {
            animator.SetFloat("isMove", Mathf.Abs(velocidadH));
            animator.SetBool("enSuelo", tocandoElSuelo);
            animator.SetFloat("velocidadY", rb.linearVelocity.y);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanciaLineaSuelo);
    }
}