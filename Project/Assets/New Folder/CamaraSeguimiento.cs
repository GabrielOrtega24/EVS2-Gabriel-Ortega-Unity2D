using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    public Transform objetivo;
    public float suavizado = 0.15f;
    public Vector3 desfase = new Vector3(0f, 1f, -10f);

    void LateUpdate()
    {
        if (objetivo == null) return;
        Vector3 posicionDeseada = objetivo.position + desfase;
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
    }
}