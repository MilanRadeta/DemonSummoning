using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Card))]
public class CardText : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    public List<TMPro.TextMeshPro> triggerNumberText;
    private Card card;

    void OnValidate()
    {
        card = GetComponent<Card>();
        SetText();
    }
    
    private void SetText()
    {
        text.text = card.cardName;
        for (int i = 0; i < triggerNumberText.Count; i++)
        {
            var text = triggerNumberText[i];
            if (i < card.triggerNumbers.Count)
            {
                var value = card.triggerNumbers[i];
                text.text = value.ToString();
                text.transform.parent.gameObject.SetActive(true);
                continue;
            }
            text.text = "";
            text.transform.parent.gameObject.SetActive(false);
        }

    }

}
