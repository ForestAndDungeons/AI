using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPatrolState : IState
{
    HunterSM _hunterSM;
    GameObject[] _hunterWaypoints;
    float _hunterStamina;
    int _currentWaypoint;
    float _hunterSpeed;
    float _hunterView;
    Hunter _hunter;
    public HPatrolState(HunterSM hunterSM, GameObject[] hunterWaypoints, float hunterStamina, int currentWaypoint, float hunterSpeed, float hunterView, Hunter hunter)
    {
        _hunterSM = hunterSM;
        _hunterWaypoints = hunterWaypoints;
        _hunterStamina = hunterStamina;
        _currentWaypoint = currentWaypoint;
        _hunterSpeed = hunterSpeed;
        _hunterView = hunterView;
        _hunter = hunter;
    }

    public void OnStart()
    {
        Debug.Log("Estoy en Patrol");
    }

    public void OnUpdate()
    {
        /* 
         if(stamina <= 0) _hunterSM.ChangeState(HunterState.HunterIdle);
         else if(EnemyInRange) _hunterSM.ChangeState(HunterState.HunterPersuit);
         else Patrol();
         
         */

        foreach (var boid in GameManager.Instance.GetAllBoids())
        {
            if (Vector3.Distance(boid.transform.position,_hunter.transform.position )<= _hunterView)
            {
                if (_hunter.GetTarget() == null)
                {
                    _hunter.SetTarget(boid.transform);
                }
                _hunterSM.ChangeState(HunterState.HunterPersuit);
            }
        }
        Patrol();
    }

    public void OnExit()
    {

    }

    void Patrol()
    {
        var currWaypoint = _hunterWaypoints[_currentWaypoint];
        Vector3 hunterDir = currWaypoint.transform.position - _hunter.transform.position;
        _hunter.transform.forward = hunterDir;
        _hunter.transform.position += _hunter.transform.forward * _hunterSpeed * Time.deltaTime;
        if (hunterDir.magnitude <=0.2f)
        {
            _currentWaypoint++;
            if (_currentWaypoint > _hunterWaypoints.Length -1)
            {
                _currentWaypoint = 0;
            }
        }
    }
}
