using UnityEngine;

public class CardEventHandler : MonoBehaviour
{
    public delegate void ClickAction(Card card);
    private Card card;


    void Awake()
    {
        card = GetComponent<Card>();
    }

    public void OnPointerClick()
    {
        if (card.FaceUp)
        {
            card.ExecuteOnClicked();
        }
    }

    public void OnPointerEnter()
    {
        if (card.FaceUp)
        {
            CardDescription.Instance.Show(card);
        }

    }

    public void OnPointerExit()
    {
        if (card.FaceUp)
        {
            CardDescription.Instance.Hide();
        }
    }
}
