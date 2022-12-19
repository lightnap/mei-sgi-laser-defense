using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float _SpawnRate = 5.0f; // Rate at which enemies spawn (in seconds / enemy).

    private float _SpawnEnemyTimer = 0.0f; 

    [SerializeField]
    private float _MinSpawnRadius = 1.0f; // Minimal distance from the tower at which an enemy may spawn. 

    [SerializeField]
    private float _MaxSpawnRadius = 2.0f; // Minimal distance from the tower at which an enemy may spawn. 

    [SerializeField]
    private int _MaxSpawnedEnemies = 10; // Maximal number of enemies spawned. 

    private int _CurrentSpawnedEnemies = 0;

    [SerializeField]
    private float _carEnemySpeed = 0.1f; 


    [SerializeField]
    private GameObject _CarEnemyObject = null; // Prefab of the car enemy. 

    //private List<Transform> enemiesList = new List<Transform>(); 

    // Start is called before the first frame update
    void Start()
    {
        _SpawnEnemyTimer = _SpawnRate; 
    }

    // Update is called once per frame
    void Update()
    {
        _SpawnEnemyTimer -= Time.deltaTime;
        if (_SpawnEnemyTimer <= 0 && _CurrentSpawnedEnemies < _MaxSpawnedEnemies) 
        {
            SpawnEnemy();
            _SpawnEnemyTimer = _SpawnRate;

        }
    }

    private void SpawnEnemy()
    {
        _CurrentSpawnedEnemies++;


        // Choose random point
        float angle = Random.Range(0.0f, 2*Mathf.PI);
        float radius = Random.Range(_MinSpawnRadius, _MaxSpawnRadius);
        Vector3 spawnPosition = new Vector3(radius * Mathf.Cos(angle) + transform.position.x, 0.0f, radius * Mathf.Sin(angle) + +transform.position.z);

        // Spawn enemy at point. 
        GameObject spawnedCar = Instantiate(_CarEnemyObject, spawnPosition, Quaternion.identity, transform);
        spawnedCar.transform.LookAt(transform);
        spawnedCar.GetComponent<EnemyCarManager>()._enemySpawner = this;
        spawnedCar.GetComponent<EnemyCarManager>()._carSpeed = _carEnemySpeed; 

    }

    public void EnemyDestroyed() 
    {
        _CurrentSpawnedEnemies--; 
    }
}
