using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDescription : MonoBehaviour
{
    public static CardDescription instance;
    public Transform cardSlot;
    public Text text;
    private RectTransform rectTransform;

    public CardDescription()
    {
        instance = this;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        this.Hide();
    }

    public void Show(Card card)
    {
        if (card.transform.position.z > 0) {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 0);
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Abs(rectTransform.anchoredPosition.y));
        } else {
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(0, -Mathf.Abs(rectTransform.anchoredPosition.y));
        }
        // if (ShouldSwitchPosition(card))
        // {
        //     // transform.localPosition = new Vector3(
        //     //     transform.localPosition.x,
        //     //     -transform.localPosition.y,
        //     //     transform.localPosition.z
        //     // );
        // }

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
