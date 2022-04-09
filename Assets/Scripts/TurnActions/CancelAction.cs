public class CancelAction : TurnAction
{

    // TODO cancel current CardAction
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
