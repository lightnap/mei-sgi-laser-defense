using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarManager : MonoBehaviour
{

    [SerializeField]
    public float _carSpeed = 5.0f;

    [SerializeField]
    private float _MaxHealth = 3.0f;
    private float _CurrentHealth = 0.0f;
    private bool _CurrentlyBeingHit = false;
    private bool _PreviouslyBeingHit = false; 

    private bool _IsBeingDestroyed = false;


    private bool _isMoving = false;

    public EnemySpawner _enemySpawner {get; set;}

    private string MODEL_NAME = "EnemyModel";

    [SerializeField]
    private WheelSpin _wheelSpin = null; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        _isMoving = true;
        _CurrentHealth =_MaxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving) 
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _carSpeed); 
        }

        if (_CurrentlyBeingHit && !_PreviouslyBeingHit) 
        {
            StopCar();
            _PreviouslyBeingHit = true;
        }
        else if (_PreviouslyBeingHit && !_CurrentlyBeingHit) 
        {
            _PreviouslyBeingHit = false;
            UnstopCar();
        }

        if (_CurrentlyBeingHit) 
        {
            _CurrentlyBeingHit = false;
        }
        if (_CurrentHealth <= 0 && !_IsBeingDestroyed) 
        {
            DestroyThisEnemy();
        }
    }

    public void StopCar() 
    {
        _isMoving = false;
        if (_wheelSpin != null) 
        {
            _wheelSpin.StopWheelsSpinning();
            Debug.Log("StoppedWHeelSPin");
        }
    }

    public void UnstopCar()
    {
        _isMoving = true;
        if (_wheelSpin != null)
        {
            _wheelSpin.StartWheelsSpinning();
            Debug.Log("UnStoppedWHeelSPin");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainBase" && !_IsBeingDestroyed) 
        {
            DestroyThisEnemy();
        }
    }

    public void DestroyThisEnemy() 
    {
        _IsBeingDestroyed = true; 
        if (_enemySpawner != null) 
        {
            _enemySpawner.EnemyDestroyed(); 
        }

        // Make model invisible. 
        transform.Find(MODEL_NAME).gameObject.SetActive(false);
        StopCar();

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

    public void DecreaseHealth() 
    {
        _CurrentlyBeingHit = true;
        _CurrentHealth -= Time.deltaTime;
    }

}
