using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Card))]
public class CardValidator : MonoBehaviour
{
    private Card card;

    void Start()
    {
        card = GetComponent<Card>();
        CheckForUnusedActions();
    }

    private void CheckForUnusedActions()
    {
        var allActions = card.GetComponents<CardAction>().ToList();
        var action = card.startingAction;
        do
        {
            allActions.Remove(action);
            action = action.Next;
        } while (action != null);

        var perCardActions = card.GetComponents<ExecutePerCard>().ToList();
        foreach (var perCardAction in perCardActions)
        {
            allActions.Remove(perCardAction.action);
        }

        if (allActions.Count > 0)
        {
            throw new MissingReferenceException("Unused actions on card " + card.name + ": " + string.Join(",", allActions.Select(c => c.GetType())));
        }

    }
}
