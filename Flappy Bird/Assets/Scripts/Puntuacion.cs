using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puntuacion : MonoBehaviour
{
    private void OnTriggerEnter2D()
    {
        GameManager.Instance.AumentarPuntuacion();
    }
}
