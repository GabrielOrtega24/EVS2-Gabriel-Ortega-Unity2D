using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento y Animación")]
    public float velocidad = 5f;
    public Animator animator;

    [Header("Configuración de Salto")]
    public float fuerzaSalto = 10f;
    public float distanciaLineaSuelo = 2.4f;
    public LayerMask capaSuelo;
    public bool tocandoElSuelo = false;

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