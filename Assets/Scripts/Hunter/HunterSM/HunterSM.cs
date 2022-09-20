using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSM 
{
    private IState _curretState;
    private Dictionary<HunterState, IState> _allHunterStates = new Dictionary<HunterState, IState>();

    public void Update()
    {
        _curretState.OnUpdate();
    }

    public void ChangeState(HunterState Hstate)
    {
        if (!_allHunterStates.ContainsKey(Hstate)) return;

        if (_curretState != null)
        {
            _curretState.OnExit();
        }

        _curretState = _allHunterStates[Hstate];
        _curretState.OnStart();
    }

    public void AddState(HunterState stateName, IState stateAction)
    {
        if (!_allHunterStates.ContainsKey(stateName))
        {
            _allHunterStates.Add(stateName, stateAction);
        }
        else
        {
            _allHunterStates[stateName] = stateAction;
        }
    }
}
