using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSteering
{
    Vector3 _velocity;
    float _maxSpeed;
    float _maxForce;
    
    public BoidSteering(Boid myBoid)
    {
        _velocity = myBoid.GetVelocity();
        _maxSpeed = myBoid.GetMaxSpeed();
        _maxForce = myBoid.GetMaxForce();
    }

    public Vector3 CalculateSteering(Vector3 desired)
    {
        return Vector3.ClampMagnitude(desired.normalized * _maxSpeed - _velocity, _maxForce);
    }
}