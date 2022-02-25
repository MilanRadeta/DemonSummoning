using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectCards : FilterCards
{
    public CardCondition condition;
    public int count = 1;
    protected List<Card> selected;
    private bool clicked;

    void OnValidate()
    {
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("SelectCards.CardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerable<Card> Cards
    {
        get
        {
            selected = new List<Card>();
            var confirmed = false;
            while (!confirmed)
            {
                clicked = false;
                var cards = Cards.Where(c => CanSelect(c));
                cards.ToList().ForEach(c => c.OnClicked += Select);
                selected.ForEach(c => c.OnClicked += Deselect);
                // yield return new WaitUntil(() => clicked);
                // TODO show choose dialog

                cards.ToList().ForEach(c => c.OnClicked -= Select);
                selected.ForEach(c => c.OnClicked -= Deselect);
            }
            return selected;
        }
    }

    private void Select(Card c)
    {
        selected.Add(c);
        clicked = true;
    }

    private void Deselect(Card c)
    {
        selected.Remove(c);
        clicked = true;
    }

    private bool CanSelect(Card c)
    {
        return !selected.Contains(c)
            && selected.Count < count
            && condition.SatisfiesRequirements(selected.Concat(new List<Card>() { c })); ;
    }

    private bool CanDeselect(Card card)
    {
        return selected.Contains(card);
    }

    private bool CanConfirm()
    {
        return condition.SatisfiesRequirements(selected);
    }
}
