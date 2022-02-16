using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] obstaclePrefab;

    private Vector3 spawnPosition;


    private float yRangeMax = 10f;
    private float yRangeMin = 3f;
    private float randomY;
    private int randomIndex;
    public float startDelay;
    public float repeatRate;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void SpawnObstacle()
    {
        if (!playerControllerScript.GameOver)
        {
            randomIndex = Random.Range(0, obstaclePrefab.Length);
            spawnPosition = RandomSpawnPosition();
            Instantiate(obstaclePrefab[randomIndex], spawnPosition, obstaclePrefab[randomIndex].transform.rotation);
        }
    }

    public Vector3 RandomSpawnPosition()
    {
        randomY = Random.Range(yRangeMin, yRangeMax);
        return new Vector3(20, randomY, 0);
    }
}
