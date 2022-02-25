using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfirmAction : TurnAction
{

    // TODO add to scene
    public override bool CanExecute()
    {
        return Card.CurrentCard?.IsConfirmable ?? false;
    }
    
    public override void OnClicked()
    {
        if (Card.CurrentCard) 
        {
            Card.CurrentCard.Confirmed = true;
        }
    }
}
