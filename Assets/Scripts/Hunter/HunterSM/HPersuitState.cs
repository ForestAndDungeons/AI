using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPersuitState : IState
{
    HunterSM _hunterSM;
    float _hunterStamina;
    Vector3 _hunterVelocity;
    float _maxSpeed;
    float _maxForce;
    Hunter _hunter;
    float _hunterView;

    public HPersuitState(HunterSM hunterSM,float hunterStamina, Vector3 hunterVelocity, float maxSpeed, Hunter hunter,float maxForce,float hunterView)
    {
        _hunterSM = hunterSM;
        _hunterStamina = hunterStamina;
        _hunterVelocity = hunterVelocity;
        _maxSpeed = maxSpeed;
        _hunter = hunter;
        _maxForce = maxForce;
        _hunterView = hunterView;
    }

    public void OnStart()
    {
        Debug.Log("Estoy en Persuit");
    }

    public void OnUpdate()
    {

        if (_hunter.GetTarget()!=null)
        {
            Persuit(_hunter.GetTarget());
        }
        else
        {
            foreach (var boid in GameManager.Instance.GetAllBoids())
            {
                if (Vector3.Distance(boid.transform.position, _hunter.transform.position) >= _hunterView)
                {
                    _hunter.SetTarget(null);
                    _hunterSM.ChangeState(HunterState.HunterPatrol);
                }
            }
        }
    }

    public void OnExit()
    {

    }

    void AddForce(Vector3 force)
    {
        _hunterVelocity += force;
        _hunterVelocity = Vector3.ClampMagnitude(_hunterVelocity, _maxSpeed);
    }

     Vector3 Persuit(Transform target)
    {
        Vector3 futurePos = target.position + _hunterVelocity * Time.deltaTime;
        Vector3 desired = (futurePos - _hunter.transform.position);
        desired.Normalize();
        desired*=_maxSpeed;
        Vector3 steering = desired - _hunterVelocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);
        return steering;
    }
}
