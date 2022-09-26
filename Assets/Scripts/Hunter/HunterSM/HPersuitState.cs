using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPersuitState : IState
{
    HunterSM _hunterSM;
    Vector3 _hunterVelocity;
    float _maxSpeed;
    float _maxForce;
    Hunter _hunter;
    float _hunterView;
    StaminaBar _staminaBar;
    float _stamina;

    public HPersuitState(HunterSM hunterSM, Vector3 hunterVelocity, float maxSpeed, Hunter hunter,float maxForce,float hunterView, StaminaBar staminaBar,float stamina)
    {
        _hunterSM = hunterSM;
        _hunterVelocity = hunterVelocity;
        _maxSpeed = maxSpeed;
        _hunter = hunter;
        _maxForce = maxForce;
        _hunterView = hunterView;
        _staminaBar = staminaBar;
        _stamina = stamina;
    }

    public void OnStart()
    {
        Debug.Log("Estoy en Persuit");
    }

    public void OnUpdate()
    {

        if (_staminaBar.currentStamina <= 0)
        {
            _hunterSM.ChangeState(HunterState.HunterIdle);
        }
        else
        {
            if (_hunter.GetTarget()!=null)
            {
                AddForce(Persuit(_hunter.GetTarget()));

                if (Vector3.Distance(_hunter.GetTarget().transform.position, _hunter.transform.position) > _hunterView)
                {
                        _hunter.SetTarget(null);
                        _hunterSM.ChangeState(HunterState.HunterPatrol);
                }
            }
            else
            {
                _hunterSM.ChangeState(HunterState.HunterPatrol);
            }

            _hunter.transform.position +=_hunterVelocity * Time.deltaTime;
            _hunter.transform.forward = _hunterVelocity;
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
        _staminaBar.UseStamina(_stamina * 1.5f);
        return steering;
    }
}
