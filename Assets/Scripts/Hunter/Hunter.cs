using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HunterState {HunterIdle,HunterPatrol,HunterPersuit}
public class Hunter : MonoBehaviour
{
    [SerializeField] HunterSM _hunterSM;
    [SerializeField] StaminaBar _staminaBar;

    [Header("Waypoints")]
    [SerializeField] GameObject[] _hunterWaypoints;
    int _currentWaypoint;

    [Header("Stamina")]
    [SerializeField] Vector3 _sliderOffset;
    float _restTimer;
    [SerializeField] float _hunterRestTime;


    [Header("Chase Parameters")]
    [SerializeField] float _hunterView;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _maxForce;
                     Vector3 _hunterVelocity;
    [SerializeField] float _hunterSpeed;
    [SerializeField] Transform _target;
    [SerializeField] float _stamina;


    private void Start()
    {
        _hunterSM = new HunterSM();

        _hunterSM.AddState(HunterState.HunterIdle, new HIdleState(_hunterSM,_staminaBar,_stamina,_restTimer,_hunterRestTime));
        _hunterSM.AddState(HunterState.HunterPatrol, new HPatrolState(_hunterSM, _hunterWaypoints, _currentWaypoint, _hunterSpeed, _hunterView,this, _staminaBar,_stamina));
        _hunterSM.AddState(HunterState.HunterPersuit, new HPersuitState(_hunterSM, _hunterVelocity, _maxSpeed,this,_maxForce,_hunterView, _staminaBar,_stamina));

        _hunterSM.ChangeState(HunterState.HunterPatrol);
    }

    private void Update()
    {
        _hunterSM.Update();
        _staminaBar.staminaSlider.transform.position = this.transform.position + _sliderOffset;
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
