using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectCards : CardAction
{
    public List<Card> Selected { get; set; }
    public GetCards CardsGetter;
    public CardCondition condition;
    public int count = 1;
    private Card selectedCard;

    void OnValidate()
    {
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("SelectCards.CardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerator Execute()
    {
        Selected = new List<Card>();
        card.Confirmed = false;

        while (!card.Confirmed)
        {
            card.IsConfirmable = CanConfirm();
            selectedCard = null;
            var cards = CardsGetter.Cards.Where(c => CanSelect(c)).ToList();
            var selected = Selected.ToList();
            cards.ForEach(c => c.OnClicked += Select);
            selected.ForEach(c => c.OnClicked += Deselect);
            yield return new WaitUntil(() => selectedCard != null || card.Confirmed);
            cards.ForEach(c => c.OnClicked -= Select);
            selected.ForEach(c => c.OnClicked -= Deselect);
        }

        card.Confirmed = false;
        yield return ExecuteNext();
    }

    private void Select(Card c)
    {
        Selected.Add(c);
        c.Selected = true;
        selectedCard = c;
    }

    private void Deselect(Card c)
    {
        Selected.Remove(c);
        c.Selected = false;
        selectedCard = c;
    }

    private bool CanSelect(Card c)
    {
        return !Selected.Contains(c)
            && Selected.Count < count
            && condition.SatisfiesRequirements(Selected.Concat(new List<Card>() { c })); ;
    }

    private bool CanDeselect(Card card)
    {
        return Selected.Contains(card);
    }

    private bool CanConfirm()
    {
        return condition.SatisfiesRequirements(Selected);
    }
}
