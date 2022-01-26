using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Player Owner { get; set; }
    public CardAffinity affinity;
    public CardType type;
    public List<int> triggerNumbers;
    public TMPro.TextMeshPro text;
    public CardAction[] actions;

    public void Execute(int roll)
    {
        if (CanExecute(roll))
        {
            Execute();
        }
    }
    
    public void Execute()
    {
        foreach (var action in actions)
        {
            if (!action.Execute()) {
                break;
            }
        }
    }

    public bool CanExecute(int roll)
    {
        return triggerNumbers.Contains(roll);
    }
}
