using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIdleState : IState
{
    HunterSM _hunterSM;
    StaminaBar _staminaBar;
    float _stamina;
    float _restTimer;
    float _hunterRestTime;

    public HIdleState(HunterSM hunterSM,StaminaBar staminaBar,float stamina,float restTimer,float hunterRestTime)
    {
        _hunterSM = hunterSM;
        _staminaBar = staminaBar;
        _stamina = stamina;
        _restTimer = restTimer;
        _hunterRestTime = hunterRestTime; 
    }

    public void OnStart()
    {
        Debug.Log("Estoy en idle");
    }

    public void OnUpdate()
    {
       /* _restTimer -= Time.deltaTime;

        if (_restTimer <= 0.0f)
        {
            _restTimer = _hunterRestTime;
        */
            OnRest();

        if (_staminaBar.currentStamina >= _staminaBar.maxStamina)
        {
            _hunterSM.ChangeState(HunterState.HunterPatrol);
        }
    }

    void OnRest()
    {
        Debug.Log("estoy descansando");
        _staminaBar.AddStamina(_stamina*2);
    }

    public void OnExit()
    {

    }
}
