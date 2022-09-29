using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidArrive
{
    BoidSteering _steering;
    Transform _myTransform;
    Vector3 _velocity;
    float _maxSpeed;
    float _maxForce;
    float _viewRadius;

    public BoidArrive(Boid myBoid)
    {
        _myTransform = myBoid.transform;
        _velocity = myBoid.GetVelocity();
        _maxSpeed = myBoid.GetMaxSpeed();
        _maxForce = myBoid.GetMaxForce();
        _viewRadius = myBoid.GetViewRadius();
        _steering = myBoid.GetSteering();
    }

    public Vector3 Arrive()
    {
        Vector3 desired = Vector3.zero;
        GameObject closeFood = null;

        foreach (var food in GameManager.Instance.GetAllFood())
        {
            if (Vector3.Distance(food.transform.position, _myTransform.position) <= _viewRadius)
            {
                closeFood = food;
                Debug.Log("FOOD");
            }
        }

        if(closeFood != null)
        {
            desired = closeFood.transform.position - _myTransform.position;

            if (desired.magnitude <= _viewRadius)
            {
                desired = desired.normalized * (_maxSpeed * (Vector3.Distance(closeFood.transform.position, _myTransform.position) / _viewRadius));
                desired = Vector3.ClampMagnitude(desired - _velocity, _maxForce);
            }
            else
            {
                desired = _steering.CalculateSteering(desired);
            }
        }
        return desired;
    }
}