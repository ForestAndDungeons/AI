using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HunterState {HunterIdle,HunterPatrol,HunterPersuit}
public class Hunter : MonoBehaviour
{
    [SerializeField] HunterSM _hunterSM;

    [Header("Stamina")]
    [SerializeField] StaminaBar _staminaBar;
    [SerializeField] Vector3 _sliderOffset;
    [SerializeField] float _restStamina;

    [Header("Hunter Parameters")]
    [SerializeField] float _hunterView;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _maxForce;
                     Vector3 _hunterVelocity;
    [SerializeField] float _hunterSpeed;
    [SerializeField] float _persuitStamina;

    [Header("Patrol Parameters")]
    int _currentWaypoint;
    [SerializeField] Transform _target;
    [SerializeField] float _patrolStamina;


    private void Start()
    {
        _hunterSM = new HunterSM();

        _hunterSM.AddState(HunterState.HunterIdle, new HIdleState(_hunterSM,_staminaBar, _restStamina));
        _hunterSM.AddState(HunterState.HunterPatrol, new HPatrolState(_hunterSM, _currentWaypoint, _hunterSpeed, _hunterView,this, _staminaBar, _patrolStamina));
        _hunterSM.AddState(HunterState.HunterPersuit, new HPersuitState(_hunterSM, _hunterVelocity, _maxSpeed,this,_maxForce,_hunterView, _staminaBar, _persuitStamina));

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

    public Vector3 GetVelocity()
    {
        return _hunterVelocity;
    }
}
