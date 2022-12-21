using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTowerManger : MonoBehaviour
{

    [SerializeField]
    private int _maximumHealth = 10;

    private int _currentHealth = 1;

    private string MODEL_NAME = "TowerModel";

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
            DestroyThisTower();
            // Trigger gamer over screen or something.
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
            _currentHealth--;
            Debug.Log("We have a hit");
        }

    }
}
