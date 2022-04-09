using System.Collections;
using System.Linq;
using UnityEngine;

public class RollDiceAction : TurnAction
{
    
    private Card card;

    public override IEnumerator Execute()
    {
        yield return Game.RollDice();
        var sum = Game.dice.Sum;
        var players = Players.Instance.GetPlayersFromActivePlayer();
        foreach (var player in players)
        {
            var cards = player.OpenCards.Where(c => c.CanExecute(sum)).ToList();
            var filteredCards = cards.Where(c => c.SatisfiesCondition).ToList();
            while (filteredCards.Count() > 0) {
                filteredCards.ForEach(c => c.OnClicked += Choose);
                yield return new WaitUntil(() => card != null);
                filteredCards.ForEach(c => c.OnClicked -= Choose);
                yield return card.Execute();
                // TODO handle canceling cards
                cards.Remove(card);
                card = null;
                filteredCards = cards.Where(c => c.SatisfiesCondition).ToList();
            }

        }
        yield return base.Execute();
    }

    private void Choose(Card card){
        this.card = card;
    }

}
