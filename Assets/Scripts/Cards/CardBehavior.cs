using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    protected Card card;
    protected GameController Game { get { return GameController.Instance; } }
    public CardCondition condition;

    // Start is called before the first frame update
    void Start()
    {
        card = GetComponent<Card>();
    }

    public virtual bool CanExecute(int roll)
    {
        return card.triggerNumbers.Contains(roll)
        && condition.Calculate(card.Owner.OpenCards);
    }

    public virtual void Execute()
    {
    }

}
