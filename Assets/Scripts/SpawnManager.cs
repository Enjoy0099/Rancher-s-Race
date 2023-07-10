using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 5f, repeatRate = 2f;
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<Player>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    void SpawnObstacle()
    {
        int random = Random.Range(0, obstaclePrefab.Length);

        if(!playerScript.gameOver)
        {
            Instantiate(obstaclePrefab[random], spawnPos, obstaclePrefab[random].transform.rotation);
        }
        
    }
}
