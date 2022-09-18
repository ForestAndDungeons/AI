using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoidCohesion
{
    Boid _myBoid;
    BoidSteering _steering;

    Transform _myTransform;
    float _viewRadius;

    public BoidCohesion(Boid myBoid)
    {
        _myBoid = myBoid;
        _steering = myBoid.GetSteering();
        _myTransform = myBoid.transform;
        _viewRadius = myBoid.GetViewRadius();
    }

    public Vector3 Cohesion()
    {
        Vector3 desired = Vector3.zero;
        int count = 0;

        foreach (var boid in GameManager.Instance.GetAllBoids())
        {
            if (boid == _myBoid) continue;
            {
                if (Vector3.Distance(_myTransform.position, boid.transform.position) <= _viewRadius)
                {
                    desired += boid.transform.position;
                    count++;
                }
            }
        }

        if (count == 0) return desired;
        
        desired /= count;
        desired -= _myTransform.position;

        return _steering.CalculateSteering(desired);
    }
}