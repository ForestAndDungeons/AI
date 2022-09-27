using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPatrolState : IState
{
    HunterSM _hunterSM;
    int _currentWaypoint;
    float _hunterSpeed;
    float _hunterView;
    Hunter _hunter;
    StaminaBar _staminaBar;
    float _patrolStamina;
    public HPatrolState(HunterSM hunterSM,  int currentWaypoint, float hunterSpeed, float hunterView, Hunter hunter, StaminaBar staminaBar,float patrolStamina)
    {
        _hunterSM = hunterSM;
        _currentWaypoint = currentWaypoint;
        _hunterSpeed = hunterSpeed;
        _hunterView = hunterView;
        _hunter = hunter;
        _staminaBar = staminaBar;
        _patrolStamina = patrolStamina;
    }

    public void OnStart()
    {
        Debug.Log("Estoy en Patrol");
    }

    public void OnUpdate()
    {
        if (_staminaBar.currentStamina <= 0)
        {
            _hunterSM.ChangeState(HunterState.HunterIdle);
        }
        else
        {
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
    }

    public void OnExit()
    {
        Debug.Log("Patrol Exit");
    }

    void Patrol()
    {
        var waypointList = GameManager.Instance.GetAllWaypoints();
        var currWaypoint = waypointList[_currentWaypoint];
        Vector3 hunterDir = currWaypoint.transform.position - _hunter.transform.position;
        _hunter.transform.forward = hunterDir;
        _hunter.transform.position += _hunter.transform.forward * _hunterSpeed * Time.deltaTime;
        if (hunterDir.magnitude <=0.2f)
        {
            _currentWaypoint++;
            GameManager.Instance.RemoveWaypoint(waypointList[_currentWaypoint - 1]);
            if (_currentWaypoint > waypointList.Count -1) 
            {
                _currentWaypoint = 0;
            }
        }
        _staminaBar.UseStamina(_patrolStamina);
    }
}
