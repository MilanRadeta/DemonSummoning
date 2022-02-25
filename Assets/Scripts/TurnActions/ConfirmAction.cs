using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfirmAction : TurnAction
{

    public override bool CanExecute()
    {
        return CardAction.CurrentAction?.IsConfirmable ?? false;
    }
    
    public override void OnClicked()
    {
        if (CardAction.CurrentAction) 
        {
            CardAction.CurrentAction.Confirmed = true;
        }
    }
}
