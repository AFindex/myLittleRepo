using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFSM : IFsm<IFsmState<BaseItem>>
{
    public override IFsm<IFsmState<BaseItem>> Create(string fsmName, params IFsmState<BaseItem>[] states)
    {
        SkillFSM fsm =new SkillFSM();
        this.fsmName = fsmName;
        this.allStates = new List<IFsmState<BaseItem>>();
        this.currentState = states[0];
        foreach (var state in states)
        {
            this.allStates.Add(state);
        }
        
        return fsm;
    }

    public override void FsmStart<TA>()
    {
        foreach (var state in this.allStates)
        {
            if (state.GetType() == typeof(TA))
            {
                currentState = state;
            }
        }
        isStart = true;
    }

    public override void FsmEnd()
    {
        isStart = false;
    }
}
