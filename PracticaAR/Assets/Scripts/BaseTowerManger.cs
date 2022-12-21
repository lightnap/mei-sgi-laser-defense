using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTowerManger : MonoBehaviour
{

    [SerializeField]
    private float _maximumHealth = 10.0f;

    private float _currentHealth = 1.0f;

    [SerializeField]
    private float _damagePerEnemyHit = 1.0f;
    private bool _CurrentlyBeingHit = false;

    private string MODEL_NAME = "TowerModel";

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0.0f) 
        {
            DestroyThisTower();
            // Trigger gamer over screen or something.
        }
        if (_CurrentlyBeingHit)
        {
            _CurrentlyBeingHit = false;
        }
    }

    public void DestroyThisTower()
    {

        // Make model invisible. 
        transform.Find(MODEL_NAME).gameObject.SetActive(false);

        // Play explosion effect and sound or something. 
        StartCoroutine(WaitToDestroy());
    }

    // Wait until explosion is over, then destroy object. 
    IEnumerator WaitToDestroy()
    {
        transform.Find("BigDeathExplosionPfx").gameObject.SetActive(true);


        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _currentHealth-= _damagePerEnemyHit;
            Debug.Log("We have a hit");
        }

    }

    public void DecreaseHealth()
    {
        _CurrentlyBeingHit = true;
        _currentHealth -= Time.deltaTime;
    }
}
