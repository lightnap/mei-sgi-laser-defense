using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRotor : MonoBehaviour
{

    [SerializeField]
    private float _spinSpeed = 5.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * _spinSpeed * Time.deltaTime * _spinSpeed); 
    }
}
