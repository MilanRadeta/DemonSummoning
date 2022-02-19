using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CancelAction : TurnAction
{
    public override bool CanExecute()
    {
        return IsCurrentActionCancelable;
    }
    
    public override void OnClicked()
    {
        if (CanExecute())
        {
            Cancel();
        }
    }
}
