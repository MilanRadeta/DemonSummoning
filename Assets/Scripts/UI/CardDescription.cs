using UnityEngine;
using UnityEngine.UI;

public class CardDescription : SingletonBehaviour<CardDescription>
{
    public Transform cardSlot;
    public Text text;
    private RectTransform rectTransform;

    protected override void Start()
    {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    public void Show(Card card)
    {
        var minMaxY = card.transform.position.z > 0 ? 0 : 1;
        var anchorY = Mathf.Abs(rectTransform.anchoredPosition.y);
        anchorY *= (minMaxY > 0 ? -1 : 1);
        rectTransform.anchorMin = new Vector2(0, minMaxY);
        rectTransform.anchorMax = new Vector2(1, minMaxY);
        rectTransform.anchoredPosition = new Vector2(0, anchorY);

        var oldCard = cardSlot.GetChild(0);
        Destroy(oldCard.gameObject);
        card.CreateUiCopy(cardSlot);
        text.text = card.description;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
