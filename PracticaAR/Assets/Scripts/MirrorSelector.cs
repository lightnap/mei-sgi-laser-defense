using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSelector : MonoBehaviour
{
    // Start is called before the first frame update

    private string _currentlySelectedMirrorName = null; 

    [SerializeField]
    private Mirror[] _arrayOfMirrors = null; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.transform.tag == "MirrorTower") 
                {
                    _currentlySelectedMirrorName = hit.transform.gameObject.name; 
                    for (int index = 0; index < _arrayOfMirrors.Length; index++)
                    {
                        if (_arrayOfMirrors[index].MirrorName == _currentlySelectedMirrorName)
                        {
                            _arrayOfMirrors[index].SelectCurrentMirror(); 
                        }
                        else
                        {
                            _arrayOfMirrors[index].DeselectCurrentMirror();
                        }
                    }
                }
            }
            else 
            {
                _currentlySelectedMirrorName = null; 
            }
        }
    }
}
