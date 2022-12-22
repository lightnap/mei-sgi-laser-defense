using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mirror : MonoBehaviour
{


    public string MirrorName = null; 

    [SerializeField]
    private GameObject arrowObject = null; 

    private bool _isSelected = false; 

    [SerializeField]
    private  Slider _slider = null; 

    [SerializeField]
    private float _MaxAngleRotation = 350.0f; 

    [SerializeField]
    private float _MinAngleRotation = 290.0f; 

    private float _currentZRotationPercent = 0.7f; 

    // Start is called before the first frame update
    void Start()
    {
        float _currentZRotation = _MinAngleRotation * (1.0f - _currentZRotationPercent) + _MaxAngleRotation *_currentZRotationPercent; 
        transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, _currentZRotation);
    }

    // Update is called once per frame
    void Update()
    {
        float newZRotation = transform.localEulerAngles.z; 
        //Vector3 newLocalRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        //transform.Rotate(Vector3.left * _spinSpeed);
        //newLocalRotation = newLocalRotation + Vector3.forward * _spinSpeed * Time.deltaTime; 
        //transform.localEulerAngles = newLocalRotation; 

        if (_isSelected)
        {
            _currentZRotationPercent = _slider.normalizedValue; 
            float _currentZRotation = _MinAngleRotation * (1.0f - _currentZRotationPercent) + _MaxAngleRotation * _currentZRotationPercent; 
            transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, _currentZRotation);
        }
    }

    public void SelectCurrentMirror ()
    {
        if (arrowObject != null){arrowObject.SetActive(true);}
        Debug.Log("Mirror" + MirrorName + "Selected");
        _isSelected = true; 
        _slider.normalizedValue =_currentZRotationPercent; 
        _slider.gameObject.SetActive(true); 
    }
    public void DeselectCurrentMirror ()
    {
        if (arrowObject != null){arrowObject.SetActive(false);}
        Debug.Log("Mirror" + MirrorName + "Deselected");
        _isSelected = false; 
        _slider.gameObject.SetActive(false); 
    }
}
