using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HunterState {HunterIdle,HunterPatrol,HunterPersuit}
public class Hunter : MonoBehaviour
{
    [SerializeField] HunterSM _hunterSM;
    [Header("Waypoints")]
    [SerializeField] GameObject[] _hunterWaypoints;
    int _currentWaypoint;
    [Header("Stamina")]
    [SerializeField] Slider _slider;
    [SerializeField] Vector3 _sliderOffset;
    [SerializeField] float _hunterStamina;
    [Header("Chase Parameters")]

    [Header("Hunter Parameters")]
    [SerializeField] float _hunterView;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _maxForce;
                     Vector3 _hunterVelocity;
    [SerializeField] float _hunterSpeed;
    [SerializeField] Transform _target;

    private void Start()
    {
        _hunterSM = new HunterSM();

        _hunterSM.AddState(HunterState.HunterIdle, new HIdleState(_hunterSM,_hunterStamina));
        _hunterSM.AddState(HunterState.HunterPatrol, new HPatrolState(_hunterSM, _hunterWaypoints, _hunterStamina, _currentWaypoint, _hunterSpeed, _hunterView,this));
        _hunterSM.AddState(HunterState.HunterPersuit, new HPersuitState(_hunterSM, _hunterStamina, _hunterVelocity, _maxSpeed,this,_maxForce,_hunterView));

        _hunterSM.ChangeState(HunterState.HunterPatrol);
    }

    private void Update()
    {
        _hunterSM.Update();
        _slider.transform.position = this.transform.position + _sliderOffset;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _hunterView);
    }

     public Transform GetTarget()
    {
        return _target;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
