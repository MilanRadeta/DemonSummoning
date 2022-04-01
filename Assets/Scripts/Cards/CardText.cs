using System.Collections.Generic;

[System.Serializable]
public class CardText
{
    public TMPro.TextMeshPro text;
    public List<TMPro.TextMeshPro> triggerNumberText;
    
    public void SetText(Card card)
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
