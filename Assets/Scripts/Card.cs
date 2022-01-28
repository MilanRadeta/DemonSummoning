using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Player Owner { get; set; }
    public CardAffinity affinity;
    public CardType type;
    public string cardName;
    public List<int> triggerNumbers;
    public TMPro.TextMeshPro text;
    public List<TMPro.TextMeshPro> triggerNumberText;
    public CardAction[] actions;

    void OnValidate()
    {
        text.text = cardName;
        for (int i = 0; i < triggerNumberText.Count; i++)
        {
            var text = triggerNumberText[i];
            if (i < triggerNumbers.Count)
            {
                var value = triggerNumbers[i];
                text.text = value.ToString();
                text.transform.parent.gameObject.SetActive(true);
                continue;
            }
            text.text = "";
            text.transform.parent.gameObject.SetActive(false);

        }
    }

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
            if (!action.Execute())
            {
                break;
            }
        }
    }

    public bool CanExecute(int roll)
    {
        return triggerNumbers.Contains(roll);
    }
}
