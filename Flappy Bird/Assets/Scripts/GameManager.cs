using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private TMP_Text puntuacionText;
    [SerializeField] private TMP_Text cuentaAtrasText; // Nuevo texto para la cuenta atrás
    [SerializeField] private float cuentaAtrasDuracion = 1f; // Duración de la cuenta atrás

    public bool isGameOver;
    private bool cuentaAtrasFinalizada; // Nueva variable para verificar si la cuenta atrás ha finalizado
    private int puntuacion;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Awake se hace antes del Start
    void Awake()
    {
        // Si no existe una instancia de GameManager lo creo, si ya existe lo destruye
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(CuentaAtras());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGameOver)
        {
            ReiniciarPartida();
        }
    }

    // Inicia una cuenta atras del tiempo que esta asignado (float cuentaAtrasDuracion)
   IEnumerator CuentaAtras()
{
    Player jugador = FindObjectOfType<Player>();

    // Desactiva la gravedad durante la cuenta atrás
    jugador.CambiarEstadoGravedad(false);

    for (float tiempoRestante = cuentaAtrasDuracion; tiempoRestante > 0; tiempoRestante -= Time.deltaTime)
    {
        cuentaAtrasText.text = Mathf.Ceil(tiempoRestante).ToString(); // Redondea hacia arriba para mostrar números enteros

        // Espera un pequeño retardo antes de verificar el clic izquierdo
        yield return null;

        if (Input.GetMouseButtonDown(0))
        {
            break; // Sale del bucle si se detecta un clic izquierdo
        }
    }

    cuentaAtrasText.gameObject.SetActive(false);
    cuentaAtrasFinalizada = true; // Indica que la cuenta atras ha finalizado

    // Habilita el vuelo despues de la cuenta atras
    jugador.CambiarEstadoGravedad(true);
    jugador.HabilitarVuelo();
}

    public void GameOver()
    {
        // isGameOver lo pone a true y muestra el texto
        isGameOver = true;
        gameOverText.SetActive(true);
    }

    private void ReiniciarPartida()
    {
        // Karga la escena que se le pone (Toma el indice de la escena actual)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AumentarPuntuacion()
    {
        // Suma un punto
        puntuacion++;
        puntuacionText.text = puntuacion.ToString();
    }

    public bool CuentaAtrasFinalizada()
    {
        return cuentaAtrasFinalizada;
    }
}
