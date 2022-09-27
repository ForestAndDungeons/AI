using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIdleState : IState
{
    HunterSM _hunterSM;
    StaminaBar _staminaBar;
    float _restStamina;

    public HIdleState(HunterSM hunterSM,StaminaBar staminaBar,float restStamina)
    {
        _hunterSM = hunterSM;
        _staminaBar = staminaBar;
        _restStamina = restStamina;
    }

    public void OnStart()
    {
        Debug.Log("Estoy en idle");
    }

    public void OnUpdate()
    {
        if (_staminaBar.currentStamina >= _staminaBar.maxStamina)
        {
            _hunterSM.ChangeState(HunterState.HunterPatrol);
        }
        OnRest();
    }

    void OnRest()
    {
        _staminaBar.AddStamina(_restStamina);
    }

    public void OnExit()
    {
        Debug.Log("Rest Exit");
    }
}
