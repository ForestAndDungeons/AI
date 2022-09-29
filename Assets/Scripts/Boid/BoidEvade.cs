using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidEvade
{
    BoidSteering _steering;

    Transform _myTransform;
    Vector3 _velocity;
    float _viewRadius;

    public BoidEvade(Boid myBoid)
    {
        _myTransform = myBoid.transform;
        _viewRadius = myBoid.GetViewRadius();
        _steering = myBoid.GetSteering();
    }

    public Vector3 Evade()
    {
        Vector3 desired = Vector3.zero;
        Hunter hunter = GameManager.Instance.GetHunter();
        Vector3 futurePos = hunter.transform.position + hunter.GetVelocity() * Time.deltaTime;
        
        if (Vector3.Distance(futurePos, _myTransform.position) <= _viewRadius)
        {
            desired = futurePos - _myTransform.position;
        }
        return _steering.CalculateSteering(-desired);
    }
}
