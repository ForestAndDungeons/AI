using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement
{
    Transform _myTransform;

    Vector3 _velocity;
    float _maxSpeed;
    float _maxForce;

    public BoidMovement(Boid myBoid)
    {
        _myTransform = myBoid.transform;
        _maxSpeed = myBoid.GetMaxSpeed();
        _maxForce = myBoid.GetMaxForce();

        Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * _maxSpeed;
        AddForce(randomVector);
    }

    public void Movement()
    {
        _myTransform.position += _velocity * Time.deltaTime;
        _myTransform.forward = _velocity;

        CheckBounds();
    }

    public void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    public void CheckBounds()
    {
        _myTransform.position = GameManager.Instance.ApplyBounds(_myTransform.position);
    }

    public Vector3 GetVelocity()
    {
        return _velocity;
    }
}