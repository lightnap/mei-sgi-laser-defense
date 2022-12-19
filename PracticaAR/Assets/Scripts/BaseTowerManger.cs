using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTowerManger : MonoBehaviour
{

    [SerializeField]
    private int _maximumHealth = 10;

    private int _currentHealth = 1; 
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0) 
        {
            gameObject.SetActive(false); 
            // Trigger gamer over screen or something.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _currentHealth--;
            Debug.Log("We have a hit");
        }

    }
}
