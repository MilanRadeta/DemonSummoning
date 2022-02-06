using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDescription : MonoBehaviour
{
    public static CardDescription instance;
    public Transform cardSlot;
    public TMPro.TextMeshPro text;

    public CardDescription()
    {
        instance = this;
    }

    void Start() {
        this.Hide();
    }

    public void Show(Card card)
    {
        var position = new Vector3(0, 0.75f, 2.5f);
        if (card.transform.position.z > 0)
        {
            position.y = -position.y;
        }
        transform.localPosition = position;

        var oldCard = cardSlot.GetChild(0);
        Destroy(oldCard.gameObject);
        var newCard = Instantiate(card);
        newCard.FaceUp = true;
        newCard.animator.enabled = false;
        newCard.transform.SetParent(cardSlot);
        newCard.transform.localPosition = Vector3.zero;
        newCard.transform.localRotation = Quaternion.Euler(Vector3.zero);
        newCard.transform.localScale = Vector3.one;
        
        Debug.Log(card.description);
        text.text = card.description;


        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
