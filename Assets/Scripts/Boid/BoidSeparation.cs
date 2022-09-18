using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSeparation
{
    BoidSteering _steering;

    Transform _myTransform;
    Vector3 _velocity;
    float _maxSpeed;
    float _maxForce;
    float _separationRadius;

    public BoidSeparation(Boid myBoid)
    {
        _steering = myBoid.GetSteering();
        _myTransform = myBoid.transform;
        _velocity = myBoid.GetVelocity();
        _maxSpeed = myBoid.GetMaxSpeed();
        _maxForce = myBoid.GetMaxForce();
        _separationRadius = myBoid.GetSeparationRedius();
    }

    public Vector3 Separation()
    {
        Vector3 desired = Vector3.zero;

        foreach (var boid in GameManager.Instance.GetAllBoids())
        {
            Vector3 distance = boid.transform.position - _myTransform.position;
            if (distance.magnitude <= _separationRadius)
                desired += distance;
        }

        if (desired == Vector3.zero) return desired;

        desired = -desired;

        return _steering.CalculateSteering(desired);
    }
}