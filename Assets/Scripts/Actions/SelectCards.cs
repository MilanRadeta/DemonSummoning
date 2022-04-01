using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectCards : CardAction
{
    public List<Card> Selected { get; set; }
    [NotNull]
    public GetCards CardsGetter;
    public CardCondition condition;
    public int count = 1;
    private Card selectedCard;
    private List<Card> selectables;
    private List<Card> deselectables;

    public override IEnumerator Execute()
    {
        Selected = new List<Card>();
        card.IsConfirmed = false;

        while (!card.IsConfirmed)
        {
            card.IsConfirmable = condition.SatisfiesRequirements(Selected);
            yield return WaitForSelection();
        }

        card.IsConfirmed = false;
        yield return ExecuteNext();
    }

    private IEnumerator WaitForSelection()
    {
        this.selectables = CardsGetter.Cards.Where(c => CanSelect(c)).ToList();
        this.deselectables = Selected.ToList();
        RegisterHandlers();
        selectedCard = null;
        yield return new WaitUntil(() => selectedCard != null || card.IsConfirmed);
        UnregisterHandlers();
    }

    private void RegisterHandlers()
    {
        selectables.ForEach(c => c.OnClicked += Select);
        deselectables.ForEach(c => c.OnClicked += Deselect);
    }

    private void UnregisterHandlers()
    {
        selectables.ForEach(c => c.OnClicked -= Select);
        deselectables.ForEach(c => c.OnClicked -= Deselect);
    }

    private void ToggleSelect(Card c, bool value)
    {
        if (value)
        {
            Selected.Add(c);
        }
        else
        {
            Selected.Remove(c);
        }
        c.IsSelected = value;
        selectedCard = c;
    }

    private void Select(Card c) { ToggleSelect(c, true); }
    private void Deselect(Card c) { ToggleSelect(c, false); }

    private bool CanSelect(Card c)
    {
        return !Selected.Contains(c)
            && Selected.Count < count
            && condition.SatisfiesRequirements(Selected.Concat(new List<Card>() { c })); ;
    }
}
