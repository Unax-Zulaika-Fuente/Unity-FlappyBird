using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desplazamiento : MonoBehaviour
{
    [SerializeField] private float velocidad = 2.5f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Deja el movimiento del fondo desactivado al principio
        rb.velocity = Vector2.zero;

     //   rb.velocity = Vector2.left * velocidad;
    }

    // Update is called once per frame
    void Update()
    {
        // Si la cuenta atrás ha finalizado y no es game over, activa el desplazamiento
        if (!GameManager.Instance.isGameOver && GameManager.Instance.CuentaAtrasFinalizada())
        {
            rb.velocity = Vector2.left * velocidad;
        }
        else
        {
            // De lo contrario, detén el movimiento del fondo
            rb.velocity = Vector2.zero;
        }
    }
}
