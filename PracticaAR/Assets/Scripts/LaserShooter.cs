using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    LineRenderer _lineRenderer = null;

    [SerializeField]
    private int _maxNumberBounces = 4;


    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {

        // Shoot ray. 
        RaycastHit hit;
        Vector3 hitPoint;

        int CurrentBounceIndex = 0;
        bool HasHitReflectableSurface = false;
        bool HasHit = false;
        Vector3 CurrentRayOrigin = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        Vector3 CurrentRayDirection = transform.TransformDirection(Vector3.forward);
        Vector3 NextRayDirection = new Vector3(0.0f, 0.0f, 0.0f);
        _lineRenderer.positionCount = 2;

        do
        {
            if (Physics.Raycast(CurrentRayOrigin, CurrentRayDirection, out hit, Mathf.Infinity))
            {
                HasHit = true;
                Debug.DrawRay(CurrentRayOrigin, CurrentRayDirection * hit.distance, Color.yellow);
                hitPoint = CurrentRayOrigin + CurrentRayDirection * hit.distance;
                Debug.Log("Did Hit");
                if (hit.transform.gameObject.tag == "Mirror")
                {
                    // Hit a mirror.
                    HasHitReflectableSurface = true;
                    _lineRenderer.positionCount = _lineRenderer.positionCount + 1;
                    
                    NextRayDirection = hit.transform.InverseTransformVector(CurrentRayDirection * (-1.0f));
                    NextRayDirection = new Vector3(NextRayDirection.x, -NextRayDirection.y, -NextRayDirection.z);
                    NextRayDirection = hit.transform.TransformVector(NextRayDirection); 

                    Debug.Log("Hit a mirror");

                }
                else if(hit.transform.gameObject.tag == "Enemy")
                {
                    // Hit an enemy. 
                    HasHitReflectableSurface = false;
                    Debug.Log("Hit an enemy");
                    // Call enemy funtion that hurts them. 
                    hit.transform.GetComponent<EnemyCarManager>().DecreaseHealth(); 
                }
                else
                {
                    // Hit something that is not an enemy, not a mirror. 
                    HasHitReflectableSurface = false;
                }
            }
            else
            {
                HasHit = false;
                Debug.DrawRay(CurrentRayOrigin, CurrentRayDirection * 1000, Color.white);
                hitPoint = CurrentRayOrigin + CurrentRayDirection * 1000;
                Debug.Log("Did not Hit");
            }
            _lineRenderer.SetPosition(CurrentBounceIndex, CurrentRayOrigin);
            _lineRenderer.SetPosition(CurrentBounceIndex + 1, hitPoint);

            if (HasHitReflectableSurface)
            {
                CurrentBounceIndex++;
                CurrentRayOrigin = hitPoint; // Next bounce starts where this one ends. 
                CurrentRayDirection = NextRayDirection; 
            }
        }
        while (CurrentBounceIndex <= _maxNumberBounces && HasHitReflectableSurface && HasHit);



    }
}
