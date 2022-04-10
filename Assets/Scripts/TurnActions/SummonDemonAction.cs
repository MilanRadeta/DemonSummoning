using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonDemonAction : TurnAction
{
    private Card demon;
    private List<Card> sacrifices;
    public Player ActivePlayer { get { return Players.Instance.ActivePlayer; } }

    public override bool CanExecute()
    {
        return base.CanExecute() && ActivePlayer.CanSummonDemon(Game.config.demonSacrifices);
    }

    public override IEnumerator Execute()
    {
        sacrifices.Clear();
        demon = null;
        ActivePlayer.OpenCards.ForEach(c => c.OnClicked += ToggleSacrifice);
        ActivePlayer.HandCards.ForEach(c => c.OnClicked += ChooseDemon);
        yield return new WaitUntil(() => demon != null && sacrifices.Count != Game.config.demonSacrifices);
        ActivePlayer.HandCards.ForEach(c => c.OnClicked -= ChooseDemon);
        ActivePlayer.OpenCards.ForEach(c => c.OnClicked -= ToggleSacrifice);
        Game.SummonDemon(demon, sacrifices);
        yield return base.Execute();
    }

    private void ToggleSacrifice(Card card)
    {
        if (sacrifices.Contains(card))
        {
            sacrifices.Remove(card);
            card.IsSelected = true;
        }
        else
        {
            sacrifices.Add(card);
            card.IsSelected = false;
        }
    }

    private void ChooseDemon(Card card)
    {
        if (demon != null)
        {
            demon.IsSelected = false;
        }
        demon = card;
        demon.IsSelected = true;
    }


}
