using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonDemonAction : TurnAction
{
    private Card demon;
    private List<Card> sacrifices;

    public override bool CanExecute()
    {
        return base.CanExecute() && Game.ActivePlayer.CanSummonDemon(Game.config.demonSacrifices);
    }

    public override IEnumerator Execute()
    {
        sacrifices.Clear();
        demon = null;
        Game.ActivePlayer.OpenCards.ForEach(c => c.OnClicked += ChooseSacrifice);
        Game.ActivePlayer.HandCards.ForEach(c => c.OnClicked += ChooseDemon);
        yield return new WaitUntil(() => demon != null && sacrifices.Count != Game.config.demonSacrifices);
        Game.ActivePlayer.HandCards.ForEach(c => c.OnClicked -= ChooseDemon);
        Game.ActivePlayer.OpenCards.ForEach(c => c.OnClicked -= ChooseSacrifice);
        Game.SummonDemon(demon, sacrifices);
        yield return base.Execute();
    }

    private void ChooseSacrifice(Card card)
    {
        if (sacrifices.Contains(card))
        {
            sacrifices.Remove(card);
        }
        else
        {
            sacrifices.Add(card);
        }
    }

    private void ChooseDemon(Card card)
    {
        demon = card;
    }


}
