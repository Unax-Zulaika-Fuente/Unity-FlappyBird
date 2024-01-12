using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BucleFondo : MonoBehaviour
{
    private float spriteAncho;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D sueloCollider = GetComponent<BoxCollider2D>();
        spriteAncho = sueloCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // si el sprite ha idoa a la izquierda una distancia mayor a su mismo ancho (spriteAncho), reposicionamos a la derecha del otro.
        if (transform.position.x < -spriteAncho)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        // el eje X movemos 2 veces su ancho (para ponerlo a la derecha del todo y no encima). En eje Y y Z = 0;
        transform.Translate(new Vector2(2 * spriteAncho, 0f));
    }
}
