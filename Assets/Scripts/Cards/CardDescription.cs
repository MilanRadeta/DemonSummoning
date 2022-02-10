using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDescription : MonoBehaviour
{
    public static CardDescription instance;
    public Transform cardSlot;
    public Text text;

    public CardDescription()
    {
        instance = this;
    }

    void Start()
    {
        this.Hide();
    }

    public void Show(Card card)
    {
        if (ShouldSwitchPosition(card))
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                -transform.localPosition.y,
                transform.localPosition.z
            );
        }

        var oldCard = cardSlot.GetChild(0);
        Destroy(oldCard.gameObject);
        var newCard = Instantiate(card);
        Layers.ChangeLayers(newCard.gameObject, LayerMask.NameToLayer("UI"));
        newCard.FaceUp = true;
        newCard.animator.enabled = false;
        newCard.transform.SetParent(cardSlot);
        newCard.transform.localPosition = Vector3.zero;
        newCard.transform.localRotation = Quaternion.Euler(Vector3.zero);
        newCard.transform.localScale = Vector3.one;

        text.text = card.description;


        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private bool ShouldSwitchPosition(Card card)
    {
        return Mathf.Sign(transform.localPosition.y) + Mathf.Sign(card.transform.position.z) != 0;
    }
}
