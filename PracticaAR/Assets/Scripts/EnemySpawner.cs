using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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


    private int NUMBER_OF_ENEMIES = 2; 

    [SerializeField]
    private float FLY_ENEMY_SPAWN_HEIGHT = 1.0f; 

    [SerializeField]
    private GameObject _CarEnemyObject = null; // Prefab of the car enemy. 

    [SerializeField]
    private GameObject _DroneEnemyObject = null; // Prefab of the drone. 

    private bool _spawningEnabled = false; 

    [SerializeField]
    private BaseTowerManger _baseTowerManager = null; 

    [SerializeField] 
    private TextMeshProUGUI _ScoreText = null;



    //private List<Transform> enemiesList = new List<Transform>(); 

    // Start is called before the first frame update

    private int _score = 0; 
    void Start()
    {
        _SpawnEnemyTimer = _SpawnRate; 
        _score = 0; 
        _ScoreText.SetText("Score: " +_score.ToString("D2")); 

    }

    // Update is called once per frame
    void Update()
    {
        if (_spawningEnabled)
        {
            _SpawnEnemyTimer -= Time.deltaTime;
        }

        if (_SpawnEnemyTimer <= 0 && _CurrentSpawnedEnemies < _MaxSpawnedEnemies) 
        {
            SpawnEnemy();
            _SpawnEnemyTimer = _SpawnRate;

        }
    }

    private void SpawnEnemy()
    {
        _CurrentSpawnedEnemies++;


        // Select which enemy to spawn. 
        GameObject enemyToSpawn = null;  
        int enemyToSpawnIndex = Random.Range(0, NUMBER_OF_ENEMIES);
        float spawnYCoordinate = 0.0f; 
        if (enemyToSpawnIndex == 0)
        {
            enemyToSpawn = _CarEnemyObject;
            spawnYCoordinate = transform.position.y; 
        }
        else 
        {
            enemyToSpawn = _DroneEnemyObject;
            spawnYCoordinate = transform.position.y + FLY_ENEMY_SPAWN_HEIGHT; 
        }

        // Choose random point
        float angle = Random.Range(0.0f, 2*Mathf.PI);
        float radius = Random.Range(_MinSpawnRadius, _MaxSpawnRadius);
        Vector3 spawnPosition = new Vector3(radius * Mathf.Cos(angle) + transform.position.x, spawnYCoordinate, radius * Mathf.Sin(angle) + +transform.position.z);


        // Spawn enemy at point. 
        GameObject spawnedCar = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity, transform);
        spawnedCar.transform.LookAt(transform);
        spawnedCar.GetComponent<EnemyCarManager>()._enemySpawner = this;
        spawnedCar.GetComponent<EnemyCarManager>()._carSpeed = _carEnemySpeed; 

    }

    public void EnemyDestroyed() 
    {
        _CurrentSpawnedEnemies--; 
    }

    public void EnableSpawning()
    {
        _spawningEnabled = true; 
    }
    public void DisableSpawning()
    {
        _spawningEnabled = true; 
    }

    public void EnemyDestroyedByLaser()
    {
        _score++; 
        _ScoreText.SetText("Score: " +_score.ToString("D2")); 

        if(_baseTowerManager != null)
        {
            _baseTowerManager.HealTower(); 
            _baseTowerManager.UpdateScore(_score);
        }
    }
}
