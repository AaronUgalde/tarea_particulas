using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private float startDelay=2;
    private float repeatRate=2;
    private Vector3 spawnPos;

    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.transform.position;
        InvokeRepeating("SpawnObstacle",startDelay,repeatRate);
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void SpawnObstacle()
    {
        if (!_playerController.gameOver)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
