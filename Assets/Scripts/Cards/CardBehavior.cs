using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    protected Card card;
    protected GameController game;
    public CardCondition condition;

    // Start is called before the first frame update
    void Start()
    {
        card = GetComponent<Card>();
        game = FindObjectOfType<GameController>();
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
