using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIdleState : IState
{
    HunterSM _hunterSM;
    float _hunterStamina;
    public HIdleState(HunterSM hunterSM,float hunterStamina)
    {
        _hunterSM = hunterSM;
        _hunterStamina = hunterStamina;
    }

    public void OnStart()
    {
        Debug.Log("Estoy en idle");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
