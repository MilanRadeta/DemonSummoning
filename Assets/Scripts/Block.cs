using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<Card> Cards { get { return cards.ToList(); } }
    public DeckConfig config;
    private List<Card> cards = new List<Card>();

    void OnValidate()
    {
        CardGenerator.RegenerateOnValidate(this, config?.cards, () => Init());
    }

    public void Init(IEnumerable<DeckConfig.CardCount> cardsConfig = null)
    {
        Reset();
        if (cardsConfig == null)
        {
            cardsConfig = config.cards;
        }
        cards = CardGenerator.GenerateCards(cardsConfig);
        RepositionCards();
    }

    public void AddCard(Card card)
    {
        this.cards.Add(card);
    }

    public void TakeCard(Card card)
    {
        this.cards.Remove(card);
    }

    private void Reset()
    {
        cards = new List<Card>();
        CardGenerator.Delete(this);
    }

    private void RepositionCards()
    {
        if (cards.Count > 0)
        {
            var delta = 360f / cards.Count;
            var transform = Instantiate(this.transform);
            transform.SetParent(this.transform);
            transform.localPosition = Vector3.forward;
            foreach (var card in cards)
            {
                transform.RotateAround(this.transform.position, Vector3.up, delta);

                card.transform.SetParent(this.transform);
                card.TargetPosition = transform.localPosition;
            }
            Remover.Destroy(transform.gameObject);
        }
    }


}
