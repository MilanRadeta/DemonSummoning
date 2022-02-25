using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectCards : CardAction
{
    public List<Card> Selected { get; set; }
    public override bool IsConfirmable { get { return condition.SatisfiesRequirements(Selected); } }
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
        Confirmed = false;

        while (!Confirmed)
        {
            selectedCard = null;
            var cards = CardsGetter.Cards.Where(c => CanSelect(c));
            cards.ToList().ForEach(c => c.OnClicked += Select);
            Selected.ForEach(c => c.OnClicked += Deselect);
            yield return new WaitUntil(() => selectedCard != null || Confirmed);
            cards.ToList().ForEach(c => c.OnClicked -= Select);
            Selected.ForEach(c => c.OnClicked -= Deselect);
        }
        yield return ExecuteNext();
    }

    private void Select(Card c)
    {
        Selected.Add(c);
        selectedCard = c;
    }

    private void Deselect(Card c)
    {
        Selected.Remove(c);
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
}
