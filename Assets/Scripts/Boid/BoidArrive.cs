using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidArrive
{
    BoidSteering _steering;
    Transform _myTransform;
    float _viewRadius;

    public BoidArrive(Boid myBoid)
    {
        _myTransform = myBoid.transform;
        _viewRadius = myBoid.GetViewRadius();
        _steering = myBoid.GetSteering();
    }

    public Vector3 Arrive()
    {
        Vector3 desired = Vector3.zero;

        foreach (var food in GameManager.Instance.GetAllFood())
        {
            if (Vector3.Distance(food.transform.position, _myTransform.position) <= _viewRadius)
            {
                    desired = food.transform.position - _myTransform.position;
            }
        }
        return _steering.CalculateSteering(desired);
    }
}