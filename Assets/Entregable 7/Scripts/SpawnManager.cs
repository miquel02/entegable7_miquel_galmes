using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Frim una array per ficar els dos possibles objectes que faran spawn
    public GameObject[] obstaclePrefab;

    //Variable per obtenir un lloc on fer l'spawn
    private Vector3 spawnPosition;

    //Variables per determinar els limits on faran spawn el objectes i quin objecta fara spawn
    private float yRangeMax = 10f;
    private float yRangeMin = 3f;
    private float randomY;
    private int randomIndex;

    //Variables per determinar cada quan faran spawn
    private float startDelay = 3f;
    private float repeatRate = 2f;

    //Variable per cridar la variable GameOver
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //Feim que es repetesqui l'spawn
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    //Funció per determinar de manera random quin objecta fara spawn (nomes funciona si GameOver=True)
    void SpawnObstacle()
    {
        if (!playerControllerScript.GameOver)
        {
            randomIndex = Random.Range(0, obstaclePrefab.Length);
            spawnPosition = RandomSpawnPosition();
            Instantiate(obstaclePrefab[randomIndex], spawnPosition, obstaclePrefab[randomIndex].transform.rotation);
        }
    }

    //Determinam la posició random de l'spawn
    public Vector3 RandomSpawnPosition()
    {
        randomY = Random.Range(yRangeMin, yRangeMax);
        return new Vector3(20, randomY, 0);
    }
}
