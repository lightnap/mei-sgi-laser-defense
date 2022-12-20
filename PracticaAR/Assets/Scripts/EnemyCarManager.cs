using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarManager : MonoBehaviour
{

    [SerializeField]
    public float _carSpeed = 5.0f;

    private bool _isMoving = false;

    public EnemySpawner _enemySpawner {get; set;}

    private string MODEL_NAME = "EnemyModel"; 
    
    // Start is called before the first frame update
    void Start()
    {
        _isMoving = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving) 
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _carSpeed); 
        }
    }

    public void StopCar() 
    {
        _isMoving = false;
    }

    public void UnstopCar()
    {
        _isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainBase") 
        {
            DestroyThisEnemy();
        }
    }

    public void DestroyThisEnemy() 
    {
        if (_enemySpawner != null) 
        {
            _enemySpawner.EnemyDestroyed(); 
        }

        // Make model invisible. 
        transform.Find(MODEL_NAME).gameObject.SetActive(false);
        _isMoving = false;


        // Play explosion effect and sound or something. 
        StartCoroutine(WaitToDestroy());

    }

    // Wait until explosion is over, then destroy object. 
    IEnumerator WaitToDestroy() 
    {
        transform.Find("SmallDeathExplosionPfx").gameObject.SetActive(true); 


        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
