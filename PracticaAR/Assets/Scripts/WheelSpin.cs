using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{

    private bool _wheelsSpinning = false;
    [SerializeField]
    private float _wheelSpeed = 100.0f;
    private int _numberOfChildren = 0;

    private Transform[] _wheelsArray; 

    // Start is called before the first frame update
    void Start()
    {
        _wheelsSpinning = true;
        _numberOfChildren = transform.childCount;
        _wheelsArray = new Transform[4];
        for (int index = 0; index < _numberOfChildren; index++) 
        {
            _wheelsArray[index] = transform.GetChild(index); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_wheelsSpinning) 
        {
            for (int index = 0; index < _numberOfChildren; index++)
            {
                _wheelsArray[index].Rotate(Vector3.right * Time.deltaTime * _wheelSpeed);

                /*
                Vector3 currentRotation = _wheelsArray[index].localEulerAngles;
                float nextXRotation = currentRotation.x + _wheelSpeed * Time.deltaTime;
                //if (nextXRotation >= 90 && nextXRotation < 270) { nextXRotation += 180; }
                if (nextXRotation >= 360) { nextXRotation -= 360; }

               _wheelsArray[index].localEulerAngles = new Vector3(nextXRotation, currentRotation.y, currentRotation.z); */

            }
        }
    }

    public void StartWheelsSpinning() 
    {
        _wheelsSpinning = true;
    }

    public void StopWheelsSpinning()
    {
        _wheelsSpinning = false;
    }

}
