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
            Card.CurrentCard.IsConfirmed = true;
        }
    }
}
