using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // SerializeField == para que se pueda cambiar desde inspector (unity)
    [SerializeField] private float fuerza = 350f;

    private bool muerto = false;
    private bool puedeVolar = false;
    private Rigidbody2D jugadorRb;
    private Animator animacionJugador;

    private void Awake()
    {
        // Asignar el componente ribidBody del player
        // Se pone en el Awake y no en el Start para que lo guarde al principio y
        // de esa manera funciona correctamente el metodo CambiarEstadoGravedad(bool activarGravedad)
        jugadorRb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        animacionJugador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Entrada de teclado (0 == click izquierdo) y que no este muerto (muerto == false)
        if (puedeVolar && Input.GetMouseButtonDown(0) && !muerto)
        {
            Aleteo();
        }

        // Verificar límite superior del mapa
        VerificarLimiteSuperior();
    }

    private void Aleteo()
    {
        // Vector2 == 0 (Para poner velocidad 0 y con ello el impulso empezar de 0)
        // Al hacer click se potenciara hacia arriba (Vector2.up) con la potencia asignada (fuerza)
        // Reproduce la animacion de "Aleteo"
        jugadorRb.velocity = Vector2.zero;
        jugadorRb.AddForce(Vector2.up * fuerza);
        animacionJugador.SetTrigger("Aleteo");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BordeSuperior")
        {
            // El jugador colisiono con el borde superior y con ello no puede subir mas
        }
        else
        {
            // El jugador colisionó con otro objeto, lo marcamos como muerto
            muerto = true;
            animacionJugador.SetTrigger("Muerto");
            GameManager.Instance.GameOver();
        }
    }

    private void VerificarLimiteSuperior()
    {
        GameObject limiteSuperior = GameObject.FindGameObjectWithTag("UpperLimit");

        if (limiteSuperior != null)
        {
            Collider2D colliderSuperior = limiteSuperior.GetComponent<Collider2D>();

            if (colliderSuperior != null && transform.position.y > colliderSuperior.bounds.max.y)
            {
                // Ajustar la posición del jugador para que esté justo en el límite superior
                Vector3 nuevaPosicion = transform.position;
                nuevaPosicion.y = colliderSuperior.bounds.max.y;
                transform.position = nuevaPosicion;
            }
        }
    }

    // metodo para habilitar el vuelo despues de la cuenta atrás
    public void HabilitarVuelo()
    {
        puedeVolar = true;
    }

    // Metodo para cambiar el estado de la gravedad
    public void CambiarEstadoGravedad(bool activarGravedad)
    {
        if (jugadorRb != null)
        {
            if (activarGravedad)
            {
                jugadorRb.gravityScale = 2f; // Restaura la gravedad normal
            }
            else
            {
                jugadorRb.gravityScale = 0f; // Desactiva temporalmente la gravedad
            }
        }
        else
        {
            Debug.LogWarning("jugadorRb es nulo. No se puede cambiar la gravedad.");
        }
    }
}
