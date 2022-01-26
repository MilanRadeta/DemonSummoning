using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<Card> Cards { get { return cards.ToList(); } }
    private List<Card> cards = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void AddCard(Card card)
    {
        this.cards.Add(card);
    }

    public void TakeCard(Card card)
    {
        this.cards.Remove(card);
    }

}
