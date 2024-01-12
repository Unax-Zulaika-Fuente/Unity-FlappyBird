using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;


public class ColaDeObstaculos : MonoBehaviour
{
    [SerializeField] private GameObject obstaculosPrefab;
    [SerializeField] private int tamañoCola = 5;
    [SerializeField] private float tiempoSpawn = 2.5f;
    [SerializeField] private float xPosicionSpawn = 10f;
    [SerializeField] double minPosicionY = -1;
    [SerializeField] double maxPosicionY = 3;

    private float tiempoTranscurrido = 2.5f;
    private int conteoObstaculos;
    private GameObject[] obstaculos;
    // Start is called before the first frame update
    void Start()
    {
        // inicializamos el Array con el tamaño de poolSize
        obstaculos = new GameObject[tamañoCola];
        // instanciamos cada uno de los obstaculos y los guardamos en el Array de obstacles
        for (int i = 0; i < tamañoCola; i++)
        {
            obstaculos[i] = Instantiate(obstaculosPrefab);
            obstaculos[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Si la cuenta atras ha finalizado, comienza a spawnear obstculos
        if (GameManager.Instance.CuentaAtrasFinalizada() && !GameManager.Instance.isGameOver)
        {
            // suma el tiempo que pasa en cada Frame
            tiempoTranscurrido += Time.deltaTime;
            if (tiempoTranscurrido > tiempoSpawn && !GameManager.Instance.isGameOver)
            {
                SpawnObstacle();
            }
        }
    }

    private void SpawnObstacle()
    {
        // cada vez que spawnea un obstaculo se reinicia el tiempo
        tiempoTranscurrido = 0f;

        // crea un rango de spawn en Y que es aleatorio
         float ySpawnPostion = Random.Range((float)minPosicionY, (float)maxPosicionY);

        // crea un nuevo vectro con la posicion del Pipe
        Vector2 spawnPosition = new Vector2(xPosicionSpawn, ySpawnPostion);

        if (ySpawnPostion < -1)
        {
            Debug.LogWarning(ySpawnPostion);
        }
        
        // pone la nueva posicion al Pipe
        obstaculos[conteoObstaculos].transform.position = spawnPosition;

        // comprueba que la instancia este acitva
        if (!obstaculos[conteoObstaculos].activeSelf)
        {
            // muestra los obstaculos progresivamente 
            obstaculos[conteoObstaculos].SetActive(true);
        }
        conteoObstaculos++;

        // si ya va por el Pipe numero 5, vuelve al numero 1
        if (conteoObstaculos == tamañoCola)
        {
            conteoObstaculos = 0;
        }
    }
}
