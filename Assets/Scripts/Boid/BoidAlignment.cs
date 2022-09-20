using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAlignment
{
    Boid _myBoid;
    BoidSteering _steering;

    Transform _myTransform;
    Vector3 _velocity;
    float _viewRadius;

    public BoidAlignment(Boid myBoid)
    {
        _myBoid = myBoid;
        _myTransform = myBoid.transform;
        _viewRadius = myBoid.GetViewRadius();
        _steering = myBoid.GetSteering();
        _velocity = myBoid.GetVelocity();
    }

    public Vector3 Alignment()
    {
        Vector3 desired = Vector3.zero;
        int count = 0;

        foreach(var boid in GameManager.Instance.GetAllBoids())
        {
            if (boid == _myBoid) continue;
            {
                if(Vector3.Distance(boid.transform.position, _myTransform.position) <= _viewRadius)
                {
                    desired += boid.GetVelocity();
                    count++;
                }
            }
        }

        if (count == 0) return desired;

        desired /= count;

        return _steering.CalculateSteering(desired);
    }
}