using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectCards : FilterCards
{
    public CardCondition condition;
    public int count = 1;
    protected List<Card> selected;

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
            var cards = Cards;
            selected = new List<Card>();
            // TODO show choose dialog
            return selected;
        }
    }

    protected bool CanSelect(Card c)
    {
        return !selected.Contains(c)
            && selected.Count < count
            && condition.SatisfiesRequirements(selected.Concat(new List<Card>() { c })); ;
    }

    protected bool CanDeselect(Card card)
    {
        return selected.Contains(card);
    }

    protected bool CanConfirm()
    {
        return condition.SatisfiesRequirements(selected);
    }
}
