using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    private bool _isMirrorUpPressed = false;
    private bool _isMirrorDownPressed = false;

    [SerializeField]
    private float _spinSpeed = 5.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newLocalRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        if (_isMirrorDownPressed) 
        {

            //transform.Rotate(Vector3.left * _spinSpeed);
            newLocalRotation = newLocalRotation + Vector3.forward * _spinSpeed * Time.deltaTime; 
            transform.localEulerAngles = newLocalRotation; 
        }
        if (_isMirrorUpPressed) 
        {
            //transform.Rotate(Vector3.right * _spinSpeed);
            newLocalRotation = newLocalRotation + Vector3.back * _spinSpeed * Time.deltaTime;
            transform.localEulerAngles = newLocalRotation;
        }

    }

    public void SetMirrorUpButtonPressed (bool aIspressed)
    {
        _isMirrorUpPressed = aIspressed;
        Debug.Log("UpButtonPressed");
    }
    public void SetMirrorDownButtonPressed (bool aIspressed)
    {
        _isMirrorDownPressed = aIspressed;
        Debug.Log("DownButtonPressed");
    }
}
