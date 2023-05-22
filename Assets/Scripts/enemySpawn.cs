using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject enemyPoint;
    public Transform spawnPoint2;
    public GameObject enemyPoint2;
    public Transform spawnPoint3;
    public GameObject enemyPoint3;
    public Transform spawnPoint4;
    public GameObject enemyPoint4;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2.0f, 8.0f);
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        Instantiate(enemyPoint, spawnPoint);
        Instantiate(enemyPoint2, spawnPoint2);
        Instantiate(enemyPoint3, spawnPoint3);
        Instantiate(enemyPoint4, spawnPoint4);
    }
}
