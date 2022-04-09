using System.Collections;
using UnityEngine;

public class BuyBlockCardAction : TurnAction
{
    private Card card;
    
    public override bool CanExecute()
    {
        return base.CanExecute() && Players.Instance.ActivePlayer.CanBuyCard(Game.config.cardPrice);
    }

    public override IEnumerator Execute()
    {
        card = null;
        Game.block.Cards.ForEach(c => c.OnClicked += BuyBlockCardForActivePlayer);
        yield return new WaitUntil(() => card != null);
        Game.block.Cards.ForEach(c => c.OnClicked -= BuyBlockCardForActivePlayer);
        Game.BuyBlockCardForActivePlayer(card);
        yield return base.Execute();
    }

    public override void OnCancel()
    {
        Game.block.Cards.ForEach(c => c.OnClicked -= BuyBlockCardForActivePlayer);
    }
    
    private void BuyBlockCardForActivePlayer(Card card)
    {
        this.card = card;
    }

}
